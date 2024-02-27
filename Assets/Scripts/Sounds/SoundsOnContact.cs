using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SoundsOnContact : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] sounds;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length - 1)]);

    }
}
