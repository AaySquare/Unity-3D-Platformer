using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Movement
    public float moveSpeed;
    private float normalSpeed;
    public float jumpForce = 8.0f;
    public float gravity = 2.5f;

    //Dash
    public const float maxDashTime = 1.0f;
    public float dashDistance;
    private float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    public float dashSpeed;

    private Vector3 movement;
    private Vector3 dashDirection;

    private bool doubleJump;
    bool dashing = false;

    /*public Transform pivot;
    public float rotateSpeed;*/

    public CharacterController characterController;

    // Use this for initialization
    void Start ()
    {
        characterController = GetComponent<CharacterController>();
        normalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float yMove = movement.y;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        movement = (transform.right * moveHorizontal) + (transform.forward * moveVertical);
        movement = movement.normalized * moveSpeed;
        movement.y = yMove;

        if (characterController.isGrounded)
        {
            movement.y = 0f;
            doubleJump = true;  
        }
            
        if (characterController.isGrounded || doubleJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!characterController.isGrounded)
                    doubleJump = false;

                movement.y = jumpForce;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Run"))
        {
            moveSpeed = normalSpeed + 5;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        if (Input.GetButtonDown("Fire3") && dashing == false) //Left mouse button
        {
            currentDashTime = 0;
        }
        if (currentDashTime < maxDashTime)
        {
            dashDirection = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
            dashing = true;
        }
        else
        {
            dashDirection = Vector3.zero;
        }

        // Apply gravity
        movement.y = movement.y + (Physics.gravity.y * gravity * Time.deltaTime);

        // Move the controller
        characterController.Move(movement * Time.deltaTime);

        if (dashing == true)
        {
            characterController.Move(dashDirection * Time.deltaTime * dashSpeed);
            dashing = false;

        }

       /* if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(movement.x, 0f, movement.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }*/
    }
}
