using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Various noise functions
 */

public class Noise
{
    public static float SinFBM1(float amp, int numOctaves, float lacundarity, float yOffset, float speed = Mathf.PI, bool makeIrregular = true)
    {
        float value = yOffset;
        float time = Time.time*speed;
        for (int x = 0; x < numOctaves; x++)
        {
            float level = 0;
            if (makeIrregular)
                level = amp*Mathf.Sin(time*(x+1)); //give some variance to the speeds
            else
                level = amp*Mathf.Sin(time);
            value += level;
            amp *= lacundarity; //lower amplitude
            time *= lacundarity; //shift domain
        }
        return value;
    }

    public static float NoiseFBM1(float pos, float amp, int numOctaves, float lacundarity, float yOffset)
    {
        float value = yOffset;
        float posV = pos;
        for (int x = 0; x < numOctaves; x++)
        {
            float level = amp*Noise1D(posV);
            value += level;
            amp *= lacundarity; //lower amplitude
            posV *= lacundarity; //shift domain
        }
        return value;
    }

    //range 0-1
    public static float Noise1D(float x)
    {
        int i = Mathf.FloorToInt(x);
        float f = Fract(x);
        float start = Random01(i);
        float end = Random01(i+1);
        return Mathf.SmoothStep(start, end, f);
    }

    public static float Random01(float x)
    {
        return Fract(Mathf.Sin(x)*1000000f);
    }

    static float Fract(float x)
    {
        return x - Mathf.Floor(x);
    }
}
