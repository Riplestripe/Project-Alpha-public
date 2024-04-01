using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    private GameObject player;
  //  private PlayerMovement PlayerMovement;
    public float ladderspeed;
    public float ladderspeedSides = 0.1f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
     //   PlayerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnCollisionStay(Collision collision)
    {
       
            Debug.Log("Ladder");
    //   PlayerMovement.playerVelocity.y = 0;
    //   PlayerMovement.playerVelocity.y += PlayerMovement.moveDir.z * ladderspeed;
     //  PlayerMovement.speed = 1f;
    }
}

    
