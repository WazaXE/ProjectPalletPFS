using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private Animator animator;  

    bool isRoll = false;  // Boolean parameter for rolling animation

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();  // Get reference to Animator
    }

    void Update()
    {
        MovePlayer();
        Jump();
        Emote();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * moveSpeed);

        
        isRoll = (moveHorizontal != 0 || moveVertical != 0);

        
        UpdateAnimations();
    }


    void Emote()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //we can throw in functions here
            animator.SetTrigger("isEmote1");
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJump");  // Trigger the jump animation
        }
    }

    void UpdateAnimations()
    {
        animator.SetBool("isRoll", isRoll);
    }
}
