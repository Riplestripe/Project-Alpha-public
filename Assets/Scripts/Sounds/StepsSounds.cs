using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSounds : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip[] steps;
    public AudioClip[] stepsOnGlass;
    AudioSource audioSource;
    float footStepsTimer = 0;
    public float offsetTimer;
   // private PlayerMovement movement;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //movement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
  
}
