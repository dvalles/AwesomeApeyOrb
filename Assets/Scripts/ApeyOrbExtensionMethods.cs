using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class ApeyOrbExtensionMethods
{
    public static void SetWithSO(this AudioSource aSource, AudioClipSO clipSO)
    {
        aSource.clip = clipSO.clip;
        aSource.volume = clipSO.volume;
        aSource.spatialBlend = clipSO.spatialBlend;
        aSource.playOnAwake = clipSO.playOnAwake;
        aSource.loop = clipSO.loop;
        aSource.pitch = clipSO.pitch;
        aSource.spatialize = clipSO.spatialize;
    }

}
