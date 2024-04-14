using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    private PlayerMovement player;
    public bool players;
    private Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        
        
           player = other.GetComponent<PlayerMovement>();
           rb = other.GetComponent<Rigidbody>();
           player.onLadder = true ;
           players = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        
            players = false;
        rb.velocity = Vector3.zero;
            player.onLadder = false;
        
    }
}

    
