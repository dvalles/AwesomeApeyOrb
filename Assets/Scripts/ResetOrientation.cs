using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

/*
 * Controls when the player resets their orientation
 */

public class ResetOrientation : MonoBehaviour
{
    void OnEnable()
    {
        SceneLoader.sceneLoaded += ResetPlayerOrientation;
    }

    void OnDisable()
    {
        SceneLoader.sceneLoaded -= ResetPlayerOrientation;
    }

    void ResetPlayerOrientation(Scene scene, LoadSceneMode mode)
    {
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
        for (int i = 0; i < subsystems.Count; i++)
        {
            subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Device);
            subsystems[i].TryRecenter();
        }
    }
}
