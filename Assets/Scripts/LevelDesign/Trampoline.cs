using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bounces an object that hits this object
 * In that it increases its original velocity
 */

public class Trampoline : MonoBehaviour
{
    public float bounciness;

    void OnCollisionEnter(Collision col)
    {
        Vector3 vel = col.rigidbody.velocity;
        col.rigidbody.AddForce(Vector3.up * bounciness, ForceMode.Impulse);
    }
}
