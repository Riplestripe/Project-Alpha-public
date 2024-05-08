using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] GameObject waterFilter;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PlayerMovement>() != null)
        {
           // other.GetComponent<PlayerMovement>().inWater = true;
        }

        if (other.CompareTag("EyeSight") && other.GetComponentInParent<PlayerMovement>() != null)
        {
            other.GetComponentInParent<Rigidbody>().useGravity = false;
            other.GetComponentInParent<PlayerMovement>().inWater = true;
            waterFilter.SetActive(true);
            other.GetComponentInParent<Rigidbody>().mass = 1f;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EyeSight") && other.GetComponentInParent<PlayerMovement>() != null)
        {
            waterFilter.SetActive(false);
            other.GetComponentInParent<PlayerMovement>().inWater = false;
            other.GetComponentInParent<Rigidbody>().useGravity = true;
            other.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponentInParent<Rigidbody>().mass = 5f;

        }
    }
}
