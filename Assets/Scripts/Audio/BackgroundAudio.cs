using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles some background stuff
 */

public class BackgroundAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClipSO audioClip;

    void OnEnable()
    {
        FallCheck.OnFall += PlayerFell;
        SpawnPlayer.OnRespawn += PlayerSpawned;
        FinishLine.OnFinished += PlayerFinished;
    }
    
    void OnDisable()
    {
        FallCheck.OnFall -= PlayerFell;
        SpawnPlayer.OnRespawn -= PlayerSpawned;
        FinishLine.OnFinished -= PlayerFinished;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.SetWithSO(audioClip);
        audioSource.FadeIn(.5f, audioClip.volume);
    }

    void PlayerFell()
    {
        audioSource.FadeOut(.3f);
    }

    void PlayerSpawned()
    {
        audioSource.FadeIn(.3f, audioClip.volume);
    }

    void PlayerFinished()
    {
        audioSource.FadeOut(.3f);
    }
}
