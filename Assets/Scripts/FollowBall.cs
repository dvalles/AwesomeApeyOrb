using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Updates the player transform to follow the rolly ball
 */

public class FollowBall : MonoBehaviour
{
    public Transform ball;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(ball.position + Vector3.up*.2f);
    }
}
