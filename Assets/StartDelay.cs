using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class StartDelay : MonoBehaviour
{
    public AudioClip soundClip;  // Der Ton, den du abspielen m�chtest
    private AudioSource audioSource;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartCoroutine(StartDelayCoroutine());
    }

    IEnumerator StartDelayCoroutine()
    {
        yield return new WaitForSeconds(3f);  // Verz�gerung von 3 Sekunden
        audioManager.PlaySFX(audioManager.startbell);
        // Hier kannst du den Code einf�gen, der nach der Verz�gerung ausgef�hrt werden soll
    }
}

