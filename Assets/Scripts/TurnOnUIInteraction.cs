using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This will turn on UI interaction when a set few triggers happen
 * mainly opening the menu and being in the start area
 */

public class TurnOnUIInteraction : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += SetHandStatusByScene;
        UniversalMenu.menuOpened += MenuOpened;
        UniversalMenu.menuClosed += MenuClosed;
        PostFinishUI.PostFinishUIShown += FinishUIOpened;
        PostFinishUI.PostFinishUIHidden += FinishUIClosed;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SetHandStatusByScene;
        UniversalMenu.menuOpened -= MenuOpened;
        UniversalMenu.menuClosed -= MenuClosed;
        PostFinishUI.PostFinishUIShown -= FinishUIOpened;
        PostFinishUI.PostFinishUIHidden -= FinishUIClosed;
    }

    // void Update()
    // {
    //     UpdateLaserStatus();
    // }

    void UpdateLaserStatus()
    {
        if (inStart) 
        {
            TurnLaserOn();
        } 
        else if (menuOpen || finishedUIOpen)
        {
            TurnLaserOn();
        }
        else
        {
            TurnLaserOff();
        }
    }

    void TurnLaserOn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void TurnLaserOff()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    //set the status of the hand
    bool inStart = false;
    void SetHandStatusByScene(Scene newScene, LoadSceneMode mode)
    {
        if (newScene.name == "start")
            inStart = true;
        else
            inStart = false;
        UpdateLaserStatus();
    }

    //they opened a menu
    bool menuOpen = false;
    void MenuOpened()
    {
        menuOpen = true;
        UpdateLaserStatus();
    }

    //they closed a menu
    void MenuClosed()
    {
        menuOpen = false;
        UpdateLaserStatus();
    }

    //Level finished UI shown
    bool finishedUIOpen = false;
    void FinishUIOpened()
    {
        finishedUIOpen = true;
        UpdateLaserStatus();
    }

    //Level finished UI closed
    void FinishUIClosed()
    {
        finishedUIOpen = false;
        UpdateLaserStatus();
    }
}
