using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Rigidbody magnet;
    public float speed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        magnet = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        magnet.isKinematic = true;
        speed = 0f;
    }
}
    