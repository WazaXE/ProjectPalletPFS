using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public List<Transform> targets = new List<Transform>();

    public float smoothSpeed = 0.125f; 

    void Update()
    {
        if (targets.Count == 0)
            return;

        Vector3 averagePosition = Vector3.zero;
        foreach (Transform target in targets)
        {
            averagePosition += target.position;
        }
        averagePosition /= targets.Count;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, averagePosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
