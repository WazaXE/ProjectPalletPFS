using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject grassBlockPrefab; // Assign grass block prefab in the Unity inspector
    public GameObject dirtBlockPrefab; // Assign dirt block prefab in the Unity inspector
    public ChunkSize chunkSize = ChunkSize.Size32; // Chunk size dropdown menu
    public int chunkHeight = 3; // The height of the chunk
    public Texture2D noiseMap; // Assign the noise map texture in the Unity inspector

    // Enum for different chunk sizes
    public enum ChunkSize
    {
        Size16 = 16,
        Size32 = 32,
        Size64 = 64,
        Size128 = 128
    }

    // Object pools
    private List<GameObject> grassBlockPool = new List<GameObject>();
    private List<GameObject> dirtBlockPool = new List<GameObject>();

    void Start()
    {
        // Fill object pools
        FillObjectPool(grassBlockPrefab, grassBlockPool);
        FillObjectPool(dirtBlockPrefab, dirtBlockPool);

        GenerateChunk();
    }

    void FillObjectPool(GameObject prefab, List<GameObject> pool)
    {
        for (int i = 0; i < 100; i++) // You can adjust this number as needed
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    void GenerateChunk()
    {
        int size = (int)chunkSize;

        // Loop through each block position in the chunk
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                // Determine the height value from the noise map
                float heightValue = noiseMap.GetPixel(x, z).grayscale * chunkHeight;

                // Instantiate blocks up to the determined height
                for (int y = 0; y < heightValue; y++)
                {
                    // Determine the block type based on position
                    GameObject blockType = GetBlockType(x, z, y == (int)heightValue - 1);

                    // Get block from the pool
                    GameObject block = GetBlockFromPool(blockType);

                    // Set block position
                    block.transform.position = new Vector3(x, y, z);
                    block.SetActive(true);
                }
            }
        }
    }

    GameObject GetBlockFromPool(GameObject prefab)
    {
        List<GameObject> pool = prefab == grassBlockPrefab ? grassBlockPool : dirtBlockPool;
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
                return obj;
        }

        // If no inactive object found, create a new one
        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }

    GameObject GetBlockType(int x, int z, bool isTopLayer)
    {
        // If the current position is at the top layer, return grass block, otherwise return dirt block
        if (isTopLayer)
        {
            GameObject grassBlock = Instantiate(grassBlockPrefab);
            grassBlock.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            return grassBlock;
        }
        else
            return dirtBlockPrefab;
    }
}