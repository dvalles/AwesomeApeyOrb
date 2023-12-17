using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Play a sound when the ball collides with objects
 */

public class BallCollisionSound : MonoBehaviour
{
    [Header("Needed Objects")]
    public AudioSource audS;
    public AudioClipSO audioClip;
    
    // [Header("Params")]
    float baseVolume = .015f;
    
    void Start()
    {
        audS.SetWithSO(audioClip);
    }

    void OnCollisionEnter(Collision collision)
    {
        float strength = collision.relativeVelocity.magnitude;
        audS.transform.position = collision.GetContact(0).point;
        audS.volume = strength * baseVolume;
        audS.Play();
    }
}
