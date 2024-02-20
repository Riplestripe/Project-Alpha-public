using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] sounds;
    public int minTime, maxTime;
    public float time;
    public float randomtime;
    private bool isTime;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("RandomSound");

    }

    private void Update()
    {
        if (isTime)
        StartCoroutine("RandomSound");
    }

    private IEnumerator RandomSound()
    {
        isTime = false;
        randomtime = UnityEngine.Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(randomtime);
        isTime = true;
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length - 1)]);
        randomtime = UnityEngine.Random.Range(minTime, maxTime);


    }
}
