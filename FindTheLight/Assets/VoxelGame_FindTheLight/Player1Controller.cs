using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public float jumpForce = 5f;
    public float maxJumpForce = 10f;
    public float moveSpeed = 5f;

    private bool isGrounded = false;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float JumpTime;
    private bool isJumping;

    //scenechanger
    public string sceneName;

    // Teleport target
    public Transform teleportTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded && Input.GetButtonDown("Jump2"))
        {
            isJumping = true;
            jumpTimeCounter = JumpTime;
            Jump();
        }

        if (Input.GetButton("Jump2") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                Jump();
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump2"))
        {
            isJumping = false;
        }

        // Quit Stuff
        if (Input.GetButtonDown("Reset"))
        {
            Debug.Log("GameQuit");
            QuitGame();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpTimeCounter -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        // Only allow movement when the player is in the air
        if (!isGrounded)
        {
            float horizontalInput = Input.GetAxis("HorizontalTwo");
            float verticalInput = Input.GetAxis("VerticalTwo");

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (moveDirection != Vector3.zero)
            {
                rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
                animator.SetTrigger("Walk");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Win"))
        {
            // Change scene
            ChangeScene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Teleport the player to the teleportTarget position
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

    // Scene handler
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
