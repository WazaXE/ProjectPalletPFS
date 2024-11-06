using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 100f;

    private void Update()
    {
        // Get input for movement and rotation
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Calculate movement and rotation
        float moveAmount = moveInput * moveSpeed * Time.deltaTime;
        float turnAmount = turnInput * turnSpeed * Time.deltaTime;

        // Move the car based on its local forward direction
        Vector3 movement = transform.forward * moveAmount;
        transform.Translate(movement, Space.Self); // Move relative to the car's own axes

        // Rotate the car left or right
        transform.Rotate(Vector3.up, turnAmount, Space.World); // Rotate around the world's up axis
    }
}
