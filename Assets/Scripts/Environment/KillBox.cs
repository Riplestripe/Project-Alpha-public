using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public bool killPlayer;
    public bool teleportPlayer;
    public Transform teleportPoint;
    GameObject player;
    public bool inKillBox;
    private void Update()
    {
        
      
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportPoint.position;

    }

    private void Kill()
    {
        
            player.gameObject.SetActive(false);
    }

   

}
