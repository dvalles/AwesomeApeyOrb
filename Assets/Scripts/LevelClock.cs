using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Clocks the time for the level, then sends it to the global database
 */

public class LevelClock : MonoBehaviour
{
    //for those who care
    public static Action<float> ClockedTime;

    //needed
    public TextMeshProUGUI timeTextInt;
    public TextMeshProUGUI timeTextFrac;

    //internal
    float time = 0f;
    StringBuilder sBuilderInt;
    StringBuilder sBuilderFrac;

    void OnEnable()
    {
        sBuilderInt = new StringBuilder("");
        sBuilderFrac = new StringBuilder("");
        FinishLine.OnFinished += ClockTime;
        SpawnPlayer.OnRespawn += ResetTime;
    }
    
    void OnDisable()
    {
        FinishLine.OnFinished -= ClockTime;
        SpawnPlayer.OnRespawn -= ResetTime;
    }

    string s = "";
    string r = "";
    void Update()
    {
        //update time
        if (shouldCount)
            time += Time.deltaTime;

        //update clock GUI
        float displayTime = GetTime(time);
        float i = Mathf.Floor(displayTime);
        float f = MyMath.Frac(displayTime).RoundTo(2);
        sBuilderInt.Remove(0, sBuilderInt.Length); //clear them
        sBuilderFrac.Remove(0, sBuilderFrac.Length);
        sBuilderInt.Append(i); //another 64
        sBuilderFrac.Append(f); //
        sBuilderFrac.Remove(0,1);
        timeTextInt.text = sBuilderInt.ToString(); //about 64 bytes
        timeTextFrac.text = sBuilderFrac.ToString(); //
    }

    bool shouldCount = true;
    void ClockTime()
    {
        shouldCount = false;
        ClockedTime?.Invoke(time.RoundTo(3)); //for anyone who cares
    }

    void ResetTime()
    {
        time = 0f;
        shouldCount = true;
    }

    #region Helpers

    float GetTime(float time)
    {
        return time.RoundTo(2);
    }

    #endregion
}
