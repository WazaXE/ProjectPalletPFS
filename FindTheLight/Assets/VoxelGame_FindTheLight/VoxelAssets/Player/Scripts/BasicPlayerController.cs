using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    //variables
    public float moveSpeed = 2f;
    public float rotationSpeed = 90f;

    private Transform cameraPlace;

    //projectile
    public GameObject projectile;

    //Anim 
    public Animator anim;

    private void Start()
    {
        cameraPlace = Camera.main.transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {  
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 cameraForward = cameraPlace.forward;
        cameraForward.y = 0f;
        Vector3 moveDirection = cameraForward * verticalInput + cameraPlace.right * horizontalInput;
        moveDirection.Normalize();
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetTrigger("W");
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetTrigger("S");
        }

        //We shoot
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
            
        //We wack
        if (Input.GetButtonDown("Wack"))
        {
            anim.SetTrigger("Wack");
        }


        float snapTurnInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            snapTurnInput = -100f; // left Turn
        }
        else if (Input.GetKey(KeyCode.D))
        {
            snapTurnInput = 1f; // right Trun
        }

        if (Mathf.Abs(snapTurnInput) > 0.1f)
        {
            // Rotate around the vertical axis??
            float rotationAmount = snapTurnInput * rotationSpeed * Time.deltaTime;
            Vector3 rotationVector = Vector3.up * rotationAmount;
            Quaternion rotation = Quaternion.Euler(rotationVector);
            transform.rotation *= rotation;
        }




    }




}
