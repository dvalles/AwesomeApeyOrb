using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * anchors the joystick to a position
 */

public class JoystickAnchor : MonoBehaviour
{
    public Transform anchor;

    void LateUpdate()
    {
        return;
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;       
    }
}
