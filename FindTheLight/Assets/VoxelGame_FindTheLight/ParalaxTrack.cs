using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxTrack : MonoBehaviour
{
    public Transform[] objectsToFollow;
    public float parallaxFactor = 0.5f; // parallax strength
    public float verticalOffset = 2f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (objectsToFollow.Length == 0)
            return;

        float averageY = 0f;
        int activeObjectCount = 0;

        foreach (Transform obj in objectsToFollow)
        {
            if (obj.gameObject.activeSelf)
            {
                averageY += obj.position.y;
                activeObjectCount++;
            }
        }

        if (activeObjectCount > 0)
        {
            averageY /= activeObjectCount;
            Vector3 parallaxOffset = new Vector3(0, (transform.position.y - averageY) * parallaxFactor, 0);
            transform.position = initialPosition + parallaxOffset;
        }
    }
}
