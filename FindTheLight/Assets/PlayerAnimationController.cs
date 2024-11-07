using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Movement
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsWalk", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsWalk", false);
        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJump", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("IsJump", false);
        }

        //Shoot
        if (Input.GetMouseButtonDown(0)) 
        {
            animator.SetBool("IsShoot", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsShoot", false);
        }
    }
}
