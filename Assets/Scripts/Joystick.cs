using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Class to output the direction that the joystick is facing
 */

public class Joystick : MonoBehaviour
{
    //events
    public Action<Vector3> OnJoyStickDir;

    //internal
    bool watchFlag = false;
    Vector3 startForward;
    Quaternion startRot;

    [Header("Params")]
    public float speed = 1;
    public Transform anchor;

    // Start is called before the first frame update
    void Start()
    {
        startForward = transform.forward;
        startRot = transform.localRotation;
    }

    // Update is called once per frame
    // void Update()
    void LateUpdate()
    {
        if (Time.timeScale == 0)
            return;

        transform.position = anchor.position;
        if (watchFlag) //output direction of joystick
        {
            Vector3 joystickDir = Vector3.ProjectOnPlane(transform.up, Vector3.up);
            OnJoyStickDir?.Invoke(joystickDir);
        }
        else //move stick back to center
        {
            // float angle = 1f + (Quaternion.Angle(transform.localRotation, startRot) / 90);
            // float angleSquared = Mathf.Pow(angle, 3.8f);
            // transform.localRotation = Quaternion.RotateTowards(transform.localRotation, startRot, speed * angleSquared * Time.deltaTime);
            float angle = 1f + (Quaternion.Angle(transform.rotation, anchor.rotation) / 90);
            float angleSquared = Mathf.Pow(angle, 3.8f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, anchor.rotation, speed * angleSquared * Time.deltaTime);
        }
    }

    public void WatchJoystick()
    {
        watchFlag = true;
    }

    public void StopWatchingJoystick()
    {
        watchFlag = false;
    }
}
