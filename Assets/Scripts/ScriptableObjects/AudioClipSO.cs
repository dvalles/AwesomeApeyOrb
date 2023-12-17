using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hold an audio clip
 */

[CreateAssetMenu(fileName = "clip", menuName = "ScriptableObjects/AudioClipSO", order = 1)]
public class AudioClipSO : ScriptableObject
{
    public AudioClip clip;
    [Range(0,1)]
    public float volume;
    [Range(0,1)]
    public float spatialBlend;
    [Range(0,1)]
    public float pitch = 1f;
    public bool playOnAwake;
    public bool loop;
    public bool spatialize = false;
}
