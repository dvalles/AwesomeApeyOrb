using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Checks if the player has fallen through fall boundary
 */

public class FallCheck : MonoBehaviour
{
    public static Action OnFall;

    void Start()
    {
        // transform.position = new Vector3(0, -40, 0);
    }

    void OnTriggerEnter(Collider Col)
    {
        OnFall?.Invoke();
    }
}
