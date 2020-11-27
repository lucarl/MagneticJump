using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Vector3 direction;
    
    //Set initial direction to be up
    void Start()
    {
        direction = Vector3.up;
    }

    //Move elevator between y = 10 -> 14
    void Update()
    {
        //Control direction of elevator
        if (transform.position.y > 14)
        {
            direction = Vector3.down;
        }
        if (transform.position.y < 10)
        {
            direction = Vector3.up;
        }
        
        //Move elevator in the set direction
        transform.Translate(direction * (Time.deltaTime*1.3f));
    }
}
