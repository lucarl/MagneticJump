using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAttraction : MonoBehaviour
{
    //How much a player is pulled towards a magent
    private const float ForceFactor = 150f;
    
    private GameObject player;
    private bool inField;


    //Allow attraction if a player triggers sphere collider area
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inField = true;
            player = GameObject.Find("Player");
        }
    }

    //Remove attraction when player leaves
    private void OnTriggerExit(Collider other)
    {
        inField = false;
    }

    //Attract player if in sphere collider area
    private void Update()
    {
        if (inField)
        {
            player.GetComponent<Rigidbody>().AddForce((transform.position - player.transform.position) * (ForceFactor * Time.smoothDeltaTime));
        }
    }
}
