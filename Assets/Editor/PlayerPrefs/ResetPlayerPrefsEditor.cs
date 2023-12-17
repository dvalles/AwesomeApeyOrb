using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Reset all the player prefs
 */

public class ResetPlayerPrefsEditor
{
    [MenuItem("PlayerPrefs/Reset All")]
    public static void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
