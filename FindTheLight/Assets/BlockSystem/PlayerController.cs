using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 3.0f;

    void Update()
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Move the player
        transform.Translate(movement, Space.Self);

        // Rotation input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player based on mouse input
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);
        transform.Rotate(Vector3.left, mouseY * rotationSpeed);

        // Clamp the vertical rotation to prevent flipping
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -80.0f, 80.0f); // Adjust the clamp range as needed
        transform.localEulerAngles = currentRotation;
    }
}