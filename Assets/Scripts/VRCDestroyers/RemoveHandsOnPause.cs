using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hide the players hands if they open universal menu
 */

public class RemoveHandsOnPause : MonoBehaviour
{
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    // bool handsShowing = true;
    void Update()
    {
        if (OVRManager.hasInputFocus)
        {
            rend.enabled = true;
            // Debug.Log("has input focus");
            // if (!handsShowing)
            // {
            //     rend.enabled = true;
            //     handsShowing = false;
            // }
        }
        else
        {
            rend.enabled = false;
            // if (handsShowing)
            // {
            //     rend.enabled = false;
            //     handsShowing = true;
            // }
        }
    }
}
