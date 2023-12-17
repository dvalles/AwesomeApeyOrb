using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pauses the game when the universal menu is opened
 * Unless its multiplayer
 */

public class PauseOnUniversalMenu : MonoBehaviour
{
    public bool isMultiplayer = false;

    // void OnEnable()
    // {
    //     XRControllerInput.rightMenuButtonPressed += MenuPressed;
    // }
    
    // void OnDisable()
    // {
    //     XRControllerInput.rightMenuButtonPressed -= MenuPressed;
    // }

    void Update()
    {
        if (isMultiplayer)
            return;

        if (OVRManager.hasInputFocus)
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
        else
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
    }

    // bool isOpen = false;
    // void MenuPressed()
    // {
    //     OVRManager.
    // }

}
