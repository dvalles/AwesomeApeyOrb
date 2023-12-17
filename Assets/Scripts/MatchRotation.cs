using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Updates the rotation of an object to match the rotation of another
 */

public class MatchRotation : MonoBehaviour
{
    public Transform toFollow;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, toFollow.rotation.eulerAngles.y, 0);    
    }
}
