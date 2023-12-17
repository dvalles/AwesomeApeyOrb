using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

/*
 * Disable interaction with all UI when paused
 */

public class DisableUIOnPause : MonoBehaviour
{
    // Update is called once per frame
    bool focusedLastFrame = true;
    void Update()
    {
        if (OVRManager.hasInputFocus)
        {
            if (!focusedLastFrame) //first time focused
            {
                TurnOnUI();
                focusedLastFrame = true;
            }
        }
        else
        {
            if (focusedLastFrame) //first time unfocused
            {
                TurnOffUI();
                focusedLastFrame = false;
            }
        }
    }

    void TurnOffUI()
    {
        TrackedDeviceGraphicRaycaster[] casters = FindComponentsInScene<TrackedDeviceGraphicRaycaster>();
        foreach (TrackedDeviceGraphicRaycaster caster in casters)
        {
            caster.enabled = false;
        }
        XRRayInteractor[] rayInteractors = FindComponentsInScene<XRRayInteractor>();
        foreach (XRRayInteractor rayI in rayInteractors)
        {
            rayI.enabled = false;
        }
        XRInteractorLineVisual[] lineVisualizers = FindComponentsInScene<XRInteractorLineVisual>();
        foreach (XRInteractorLineVisual lineV in lineVisualizers)
        {
            lineV.enabled = false;
        }
        LineRenderer[] lines = FindComponentsInScene<LineRenderer>();
        foreach (LineRenderer line in lines)
        {
            line.enabled = false;
        }
    }

    void TurnOnUI()
    {
        TrackedDeviceGraphicRaycaster[] casters = FindComponentsInScene<TrackedDeviceGraphicRaycaster>();
        foreach (TrackedDeviceGraphicRaycaster caster in casters)
        {
            caster.enabled = true;
        }
        XRRayInteractor[] rayInteractors = FindComponentsInScene<XRRayInteractor>();
        foreach (XRRayInteractor rayI in rayInteractors)
        {
            rayI.enabled = true;
        }
        XRInteractorLineVisual[] lineVisualizers = FindComponentsInScene<XRInteractorLineVisual>();
        foreach (XRInteractorLineVisual lineV in lineVisualizers)
        {
            lineV.enabled = true;
        }
        LineRenderer[] lines = FindComponentsInScene<LineRenderer>();
        foreach (LineRenderer line in lines)
        {
            line.enabled = true;
        }
    }

    //Find components in a scene heirarchy
    T[] FindComponentsInScene<T>()
    {
        List<T> allObjects = new List<T>();
        
        //normal scene
        List<GameObject> rootObjectsInScene = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjectsInScene);
        for (int i = 0; i < rootObjectsInScene.Count; i++)
        {
            T[] allComponents = rootObjectsInScene[i].GetComponentsInChildren<T>(true);
            allObjects.AddRange(allComponents);
        }

        //dont destroy
        GameObject[] roots = DontDestroyOnLoadSpy.Instance.GetAllRootsOfDontDestroyOnLoad();
        for (int i = 0; i < roots.Length; i++)
        {
            T[] allComponents = roots[i].GetComponentsInChildren<T>(true);
            allObjects.AddRange(allComponents);
        }

        return allObjects.ToArray();
    }
}
