using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform groundCheckTransform;
    public LayerMask playerMask;

    private Vector2 lookDirection;
    private float lookAngle;
    
    private bool jumpKeyWasPressed;
    private bool isGrounded;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;

    private int scalings;
    public bool allowBallCreation;

    private bool increaseBallSpeed;

    //Modifier for player movement
    public float speedModifier;
    public float gravityModifier;
    public float jumpModifier;

    //Set initial values at player creation
    private void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        scalings = 0;
        allowBallCreation = true;
        increaseBallSpeed = false;
        
    }

    // Ball creation and setting movement direction ever update step
    void Update()
    {
        //Allow jump when space is pressed and we are on the ground
        if (Input.GetKeyDown(KeyCode.Space) && !(IsAwayFromGround(0.35f)))
        {
            jumpKeyWasPressed = true;
        }
        
        //-------------------------------------------------------------//
        //------------------------Ball creation------------------------//
        //-------------------------------------------------------------//
        if (Input.GetMouseButton(0))
        {
            increaseBallSpeed = true;
        }
        if (increaseBallSpeed && !Input.GetMouseButton(0))
        {
            if (scalings < 5)
            {
                transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);
                scalings++;
                if (scalings == 5)
                {
                    allowBallCreation = false;
                }
            }

            increaseBallSpeed = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (scalings > 0)
            {
                transform.localScale += new Vector3(+0.05f,+0.05f,+0.05f);
                scalings--;
                allowBallCreation = true;
            }
        }
        //-----------------------------------------------------------------//
        
        //Set direction of movement
        horizontalInput = Input.GetAxis("Horizontal");
    }
    
    // Set player physics every physics update    
    private void FixedUpdate()
    {
        //Player movement
        rigidBodyComponent.velocity = new Vector3(horizontalInput * speedModifier, rigidBodyComponent.velocity.y, 0);

        //Increased player gravity to accommodate their speed
        if (IsAwayFromGround(0.35f))
        {
            rigidBodyComponent.AddForce(Vector3.down * gravityModifier);
        }

        //Don't allow jumping twice by making an air check
        if (IsAwayFromGround(0.35f))
        {
            return;
        }

        //Make player jump when space pressed
        if (jumpKeyWasPressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * jumpModifier, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

    }
    
    //Check if ball's origin is a certain spherical distance (radius based) from the ground
    private bool IsAwayFromGround(float radiusCheck)
    {
        return (Physics.OverlapSphere(groundCheckTransform.position, radiusCheck, playerMask).Length == 0);
    }
    
    
}

    

