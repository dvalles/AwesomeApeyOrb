using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Move something back and forth along an axis
 */

public class LinearMovement : MonoBehaviour
{
    public enum Type {Physics, Transform};
    public enum Axis {x,y,z};

    public Type type = Type.Transform;
    public Axis axis = Axis.x;
    public float speed = 2f;
    public float distance = 10;
    public bool useOffset = false;
    public float offsetScale;
    public bool reverseDirection = false;

    //internal
    Vector3 startPos;
    Rigidbody rb;
    float offset;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        offset = (transform.position.x-6f)/106f; //hard coded for level 4-3
    }

    // Update is called once per frame
    void Update()
    {
        if (type != Type.Transform)
            return;

        float dir = ((int)((Time.time*speed)/distance)) % 2 == 0 ? -1f : 1f;
        float intervalVal = dir*((Time.time*speed)%distance)-(distance/2f);
        if (axis == Axis.x)
            transform.position = startPos + transform.right*intervalVal + transform.right*(distance)*(dir == -1 ? 1 : 0);
        if (axis == Axis.y)
            transform.position = startPos + transform.up*intervalVal + transform.up*(distance)*(dir == -1 ? 1 : 0);
        if (axis == Axis.z)
            transform.position = startPos + transform.forward*intervalVal + transform.forward*(distance)*(dir == -1 ? 1 : 0);
    }

    void FixedUpdate()
    {
        if (type != Type.Physics)
            return;

        float time = (Time.time*speed)+(offset*offsetScale);
        float dir = ((int)(time/distance)) % 2 == 0 ? -1f : 1f;
        float intervalVal = dir*(time%distance)-(distance/2f);

        Vector3 shift = Vector3.zero;
        if (axis == Axis.x)
            shift = transform.right*intervalVal + transform.right*(distance)*(dir == -1 ? 1 : 0);
        if (axis == Axis.y)
            shift = transform.up*intervalVal + transform.up*(distance)*(dir == -1 ? 1 : 0);
        if (axis == Axis.z)
            shift = transform.forward*intervalVal + transform.forward*(distance)*(dir == -1 ? 1 : 0);

        //reverse
        if (reverseDirection)
            shift = -shift;

        rb.MovePosition(startPos + shift);

        // if (axis == Axis.x)
        //     rb.MovePosition(startPos + transform.right*intervalVal + transform.right*(distance)*(dir == -1 ? 1 : 0));
        // if (axis == Axis.y)
        //     rb.MovePosition(startPos + transform.up*intervalVal + transform.up*(distance)*(dir == -1 ? 1 : 0));
        // if (axis == Axis.z)
        //     rb.MovePosition(startPos + transform.forward*intervalVal + transform.forward*(distance)*(dir == -1 ? 1 : 0));
    }


}
