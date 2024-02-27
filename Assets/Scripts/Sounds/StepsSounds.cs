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
    private PlayerMovement movement;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        FootStepsSound();

    }
    private void FootStepsSound()
    {
        if (!movement.isGrounded) return;

        footStepsTimer -= Time.deltaTime;
        if (footStepsTimer <= 0 && movement.movementPressed)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "ground":
                        audioSource.PlayOneShot(steps[Random.Range(0, steps.Length - 1)]);
                        break;
                    case "glass":
                        audioSource.PlayOneShot(stepsOnGlass[Random.Range(0, stepsOnGlass.Length - 1)]);
                        break;
                }



            }
            footStepsTimer = offsetTimer;

        }
    }
}
