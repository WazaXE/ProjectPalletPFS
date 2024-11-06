using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 1.0f; 
    public GameObject[] prefabs; 

    private bool isRotating = false; 
    private float targetAngle = 0; 
    private int currentPrefabIndex = 0; 

    void Start()
    {
        foreach (var prefab in prefabs)
        {
            prefab.SetActive(false);
        }
        prefabs[currentPrefabIndex].SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            targetAngle += 90;
            isRotating = true;    
            prefabs[currentPrefabIndex].SetActive(false);
            currentPrefabIndex = (currentPrefabIndex + 1) % prefabs.Length;
            prefabs[currentPrefabIndex].SetActive(true);
        }

        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targetAngle, 0), step);

            // Check if the rotation is complete
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, targetAngle, 0)) < 0.01f)
            {
                isRotating = false;
            }
        }
    }
}
