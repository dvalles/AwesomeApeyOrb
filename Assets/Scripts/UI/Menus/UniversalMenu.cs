using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Show or hide the universal menu based on input
 */

public class UniversalMenu : MonoBehaviour
{
    public static Action menuOpened;
    public static Action menuClosed;

    Transform ballCenter;

    //intenal
    Button backButton;

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    void Start()
    {   
        XRControllerInput.leftMenuButtonPressed += MenuPressed;

        GetComponent<Canvas>().worldCamera = Player.head.GetComponent<Camera>();
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { MenuPressed(); SceneLoader.LoadScene("start"); }); //back button
        ballCenter = GameObject.FindGameObjectWithTag("BallCenter").transform;
    }

    void LateUpdate()
    {
        if (isOpen)
        {
            transform.root.position = ballCenter.position + menuDistance;
        }
    }

    bool isOpen = false;
    Vector3 menuDistance;
    public void MenuPressed()
    {
        if (this == null)
            return;

        if (SceneManager.GetActiveScene().name == "start")
            return;

        if (!isOpen) //open
        {
            GetComponent<Canvas>().enabled = true;
            menuOpened?.Invoke();
            menuDistance = (Player.head.position + Player.head.forward) - ballCenter.position;
            transform.root.position = ballCenter.position + menuDistance;
            transform.root.LookAt(Player.head.transform.position);
        }
        else //close
        {
            GetComponent<Canvas>().enabled = false;
            menuClosed?.Invoke();
        }
        isOpen = !isOpen;
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetComponent<Canvas>().worldCamera = Player.head.GetComponent<Camera>();
        // Debug.Log(GetComponent<Canvas>().worldCamera.transform.name);
    }
}
