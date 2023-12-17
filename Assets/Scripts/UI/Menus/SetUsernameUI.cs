using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;
using System;

/*
 * Shows the set username UI if there is none set currently
 */

public class SetUsernameUI : MonoBehaviour
{
    public Action SetUsernameMenuShown;
    public Action SetUsernameMenuHidden;

    [Header("Needed Elements")]
    public TMP_InputField usernameField;
    public VRKeyboard.Utils.KeyboardManager keyboardManager;

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        keyboardManager.newValue += SetField;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        keyboardManager.newValue -= SetField;
    }

    Vector3 ogPos;
    Quaternion ogRot;
    void Start()
    {   
        ogPos = transform.root.position;
        ogRot = transform.root.rotation;
        HideMenu();
        GetComponent<Canvas>().worldCamera = Player.head.GetComponent<Camera>();
        keyboardManager.GetComponent<Canvas>().worldCamera = Player.head.GetComponent<Camera>();
        if (PlayerName.name == "")
            ShowMenu();
    }

    void LateUpdate()
    {

    }

    void SetField(string newVal)
    {
        usernameField.text = newVal;
    }

    GameObject leftStick, rightStick;
    public void ShowMenu()
    {
        if (this == null)
            return;

        transform.root.position = ogPos;
        transform.root.rotation = ogRot;
        // transform.root.position = Player.head.position + Vector3.ProjectOnPlane(Player.head.forward, Vector3.up);
        // transform.root.LookAt(Player.head.transform.position);
        GetComponent<Canvas>().enabled = true;
        keyboardManager.GetComponent<Canvas>().enabled = true;
        leftStick = GameObject.FindGameObjectWithTag("JoystickLeft");
        leftStick.SetActive(false);
        rightStick = GameObject.FindGameObjectWithTag("JoystickRight");
        rightStick.SetActive(false);

        SetUsernameMenuShown?.Invoke();
    }

    //Save their username
    public void SubmitClicked()
    {
        if (usernameField.text == "")
            return;

        PlayerName.SetName(usernameField.text);

        HideMenu();
    }

    public void HideMenu()
    {
        GetComponent<Canvas>().enabled = false;
        keyboardManager.GetComponent<Canvas>().enabled = false;
        transform.root.position = Vector3.up*1000f;
        leftStick?.SetActive(true);
        rightStick?.SetActive(true);
        SetUsernameMenuHidden?.Invoke();
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        keyboardManager.GetComponent<Canvas>().worldCamera = Player.head.GetComponent<Camera>();
    }
}
