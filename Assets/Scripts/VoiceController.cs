using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceController : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip[] voices;
    public float minDelay = 5f;
    public float maxDelay = 12f;

    //Private
    float timer;
    float curDelay;

    AudioSource audioSource;

    private void Start()
    {
        curDelay = Random.Range(minDelay, maxDelay);
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= curDelay)
        {
            //Play Voice here
            int randomIndex = Random.Range(0, voices.Length - 1);
            audioSource.PlayOneShot(voices[randomIndex]);
            //Debug.Log(randomIndex);

            //reset Timer
            timer = 0f;
        }
    }
}
