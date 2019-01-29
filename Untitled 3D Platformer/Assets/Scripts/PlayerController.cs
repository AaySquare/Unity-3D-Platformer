using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 2.5f;

    private Vector3 movement;
    private bool doubleJump;
    public CharacterController characterController;

    // Use this for initialization
    void Start ()
    {
        characterController = GetComponent<CharacterController>();
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

                movement.y = jumpSpeed;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15.0f;
        }
        else
        {
            moveSpeed = 10.0f;
        }
      

        // Apply gravity
        movement.y = movement.y + (Physics.gravity.y * gravity * Time.deltaTime);

        // Move the controller
        characterController.Move(movement * Time.deltaTime);

    }
}
