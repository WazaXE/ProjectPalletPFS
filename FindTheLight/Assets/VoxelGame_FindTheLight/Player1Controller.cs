using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public float jumpForce = 5f;
    public float moveSpeed = 5f;
    public float movementForce = 200f;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    public Transform cameraTransform;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    public string sceneName;
    public Transform teleportTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();

        // Jumping logic
        isGrounded = Physics.CheckSphere(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded && Input.GetButtonDown("Jump2"))
        {
            Jump();
        }

        // Quit game
        if (Input.GetButtonDown("Reset"))
        {
            Debug.Log("GameQuit");
            QuitGame();
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("HorizontalTwo");
        float verticalInput = Input.GetAxis("VerticalTwo");
        Vector3 moveDirection = (transform.right * horizontalInput + transform.forward * verticalInput).normalized;
        rb.AddForce(moveDirection * movementForce * Time.deltaTime, ForceMode.Force);

        // Optional Trigger
        if (animator != null)
        {
            if (moveDirection != Vector3.zero)
            {
                animator.SetTrigger("Walk");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            ChangeScene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (teleportTarget != null)
            {
                transform.position = teleportTarget.position;
                Debug.Log("Player teleported to: " + teleportTarget.position);
            }
            else
            {
                Debug.LogWarning("Teleport target not set!");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
