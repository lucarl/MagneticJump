using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Magnet : MonoBehaviour
{
    public Rigidbody magnet;
    public float speed;
    public GameObject attraction;
    private bool hasCollided;

    // Set the initial speed of the magnet shot from the player as a projectile
    void Start()
    {
        magnet.AddForce((transform.rotation*Quaternion.Euler(0, 0, 45))*new Vector3(1,1,1)*speed);
        hasCollided = false;
    }

    //Transform object from projectile to sticky magnet with attraction when colliding with terrain
    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;
        
        //Instantiate a new attraction child to the magnet
        GameObject child = Instantiate(attraction, magnet.transform, true);
        child.transform.position = magnet.position;

        //Make magnet act as a sticky object instead of a projectile
        magnet.isKinematic = true;
        speed = 0f;
        hasCollided = true;
    }
}
    