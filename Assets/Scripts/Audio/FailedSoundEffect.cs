using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Plays the failed sound effect
 */

public class FailedSoundEffect : MonoBehaviour
{
    Transform ball;
    AudioSource aSource;
    public AudioClipSO clip;    

    void OnEnable()
    {
        FallCheck.OnFall += PlaySound;
    }
    
    void OnDisable()
    {
        FallCheck.OnFall -= PlaySound;
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        aSource.SetWithSO(clip);
    }

    void PlaySound()
    {
        aSource.Play();
    }
}
