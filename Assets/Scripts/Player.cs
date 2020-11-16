using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform groundCheckTransform;
    private bool jumpKeyWasPressed;
    private bool isGrounded;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    public GameObject magneticPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject magneticObject = Instantiate(magneticPrefab);
            magneticObject.transform.position = transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }
    
    // Called every physics update    
    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1)
        {
            return;
        }
        
        if (jumpKeyWasPressed){
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        
        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);
    }
    
}
