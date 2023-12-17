using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Broadcast an event when player crosses the finish line
 */

public class FinishLine : MonoBehaviour
{
    //events
    public static Action OnFinished;

    void OnTriggerEnter(Collider col)
    {
        OnFinished?.Invoke();
    }
}
