using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sets the wind sound effect, raises pitch based on player speed
 */

public class WindSoundEffect : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody ballRb;
    public AudioClipSO clipSO;

    [Header("Parameters")]
    public float speedAtMaxPitch = 30f;
    public float maxPitchIncrease = .1f;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GameObject.FindWithTag("Ball").GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.SetWithSO(clipSO);
        audioSource.FadeIn(.5f, clipSO.volume);
    }

    // Update is called once per frame
    void Update()
    {
        float newVol = clipSO.volume + Mathf.Clamp01(ballRb.velocity.magnitude/speedAtMaxPitch)*maxPitchIncrease;
        float newPitch = clipSO.pitch + Mathf.Clamp01(ballRb.velocity.magnitude/speedAtMaxPitch)*maxPitchIncrease;
        audioSource.pitch = newPitch;
        audioSource.volume = newVol;
    }
}
