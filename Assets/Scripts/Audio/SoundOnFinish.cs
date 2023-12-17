using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Just plays a sound when you finish the level
 */

public class SoundOnFinish : MonoBehaviour
{
    public static Action OnCheeringDone;

    public AudioClipSO musicClip;
    public AudioClipSO cheerClip;
    public AudioClipSO popClip;

    AudioSource audSource;

    void OnEnable()
    {
        FinishLine.OnFinished += PlayFinishSounds;
    }
    
    void OnDisable()
    {
        FinishLine.OnFinished -= PlayFinishSounds;
    }

    void Start()
    {
        audSource = GetComponent<AudioSource>();
    }

    bool playingFinishSounds = false;
    async void PlayFinishSounds()
    {
        if (playingFinishSounds) return;
        playingFinishSounds = true;
        //pop sound
        audSource.SetWithSO(popClip);
        audSource.Play();
        await audSource.DonePlaying();

        //cheer sound
        audSource.SetWithSO(cheerClip);
        audSource.Play();
        await audSource.DonePlaying();

        OnCheeringDone?.Invoke();

        //music sound
        audSource.SetWithSO(musicClip);
        audSource.Play();
        await audSource.DonePlaying();
        playingFinishSounds = false;
    }
}
