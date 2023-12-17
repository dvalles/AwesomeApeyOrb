using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A pool of audio sources
 * For when you need to play random sounds
 */

public class AudioSourcePool
{
    static AudioSourcePoolI _instance;
    static bool initializedGlobal = false;
    static void GlobalInit()
    {
        if (initializedGlobal)
            return;
        _instance = new AudioSourcePoolI();
        initializedGlobal = true;
    }

    public static AudioSource Get()
    {
        GlobalInit();
        return _instance.Allocate();
    }
}

public class AudioSourcePoolI : ComponentPool<AudioSource>
{
    public override bool InUse(AudioSource comp)
    {
        return comp.isPlaying;
    }

    public override void SetUp(AudioSource comp)
    {

    }
}
