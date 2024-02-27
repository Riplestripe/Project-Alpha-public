using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public float offset = 5f;
    public Transform punktA;
    public Transform punktB;


    private void OnTriggerStay(Collider other)
    {
        other.transform.position = punktB.position;
    }
}

