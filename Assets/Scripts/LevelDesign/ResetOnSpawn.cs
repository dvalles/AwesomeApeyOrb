using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Resets the objects rotation and position when the player respawns
 */

public class ResetOnSpawn : MonoBehaviour
{
    public bool resetRotation;
    public bool resetPosition;

    //internal
    Vector3 ogPos;
    Quaternion ogRot;

    void OnEnable()
    {
        SpawnPlayer.OnRespawn += Reset;
    }
    
    void OnDisable()
    {
        SpawnPlayer.OnRespawn -= Reset;
    }

    void Start()
    {
        ogPos = transform.position;
        ogRot = transform.rotation;
    }

    void Reset()
    {
        if (resetPosition)
            transform.position = ogPos;
        if (resetRotation)
            transform.rotation = ogRot;
    }
}
