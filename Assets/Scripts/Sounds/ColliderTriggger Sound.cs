using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigggerSound : MonoBehaviour
{
    public AudioClip audioClip;
    
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip);
        Destroy(this.gameObject);
    }
}
