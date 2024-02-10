using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class StartDelay : MonoBehaviour
{
    public AudioClip soundClip;  // Der Ton, den du abspielen möchtest
    private AudioSource audioSource;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartCoroutine(StartDelayCoroutine());
    }

    IEnumerator StartDelayCoroutine()
    {
        yield return new WaitForSeconds(3f);  // Verzögerung von 3 Sekunden
        audioManager.PlaySFX(audioManager.startbell);
        // Hier kannst du den Code einfügen, der nach der Verzögerung ausgeführt werden soll
    }
}

