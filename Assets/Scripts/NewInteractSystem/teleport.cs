using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public float offset = 5f;
    public Transform punktA;
    public Transform punktB;
    GameObject player;


    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        Invoke("Teleport",5f );
    }

    private void Teleport()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        Debug.Log("Player teleport");
        player.transform.position = punktB.position + offset;
    }
}

