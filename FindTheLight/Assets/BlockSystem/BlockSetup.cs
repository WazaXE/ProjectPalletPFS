using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSetup : MonoBehaviour
{
    //Variables
    public Transform cameraTransform;
    public GameObject frontFace;
    public GameObject backFace;
    public GameObject leftFace;
    public GameObject rightFace;
    public GameObject botFace;
    public GameObject topFace;

    void Update()
    {
        // Calculate visibility of each face based on the camera's position and orientation
        frontFace.SetActive(IsFaceVisible(frontFace, Vector3.forward));
        backFace.SetActive(IsFaceVisible(backFace, Vector3.back));
        leftFace.SetActive(IsFaceVisible(leftFace, Vector3.left));
        rightFace.SetActive(IsFaceVisible(rightFace, Vector3.right));
        botFace.SetActive(IsFaceVisible(botFace, Vector3.down));
        topFace.SetActive(IsFaceVisible(topFace, Vector3.up));
    }

    bool IsFaceVisible(GameObject face, Vector3 faceDirection)
    {
        // Calculate the direction vector from the camera to the face
        Vector3 directionToFace = face.transform.position - cameraTransform.position;

        // Calculate the angle between the direction to the face and the camera's forward direction
        float angle = Vector3.Angle(directionToFace, cameraTransform.forward);

        // Check if the angle is within the camera's field of view
        if (angle < Camera.main.fieldOfView / 2f)
        {
            // Check if the face is facing towards the camera
            if (Vector3.Dot(faceDirection, directionToFace.normalized) < 0)
            {
                return true; // Face is visible
            }
        }
        return false; // Face is not visible
    }
}
