using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip startbell;
    public AudioClip EnemyGrunting;
    public AudioClip PlayerGrunting;
    public AudioClip punchingHit;
    public AudioClip punchingHit2;
    public AudioClip punchingHit3;
    public AudioClip punchingHit4;


    private void Start()
    {
        //musicSource.clip = startbell;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
