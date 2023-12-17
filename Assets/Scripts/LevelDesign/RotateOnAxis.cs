using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rotate an object on an axis
 */

public class RotateOnAxis : MonoBehaviour
{
    public enum Type {Physics, Transform}
    public enum Axis {x,y,z};

    public Axis axis = Axis.x;
    public Type type = Type.Transform;
    public float speed = 2f;
    public bool useOffset = false;
    public float offset;

    Rigidbody rb;

    void Start()
    {
        if (type != Type.Transform)
            rb = GetComponent<Rigidbody>();

        if (!useOffset)
            return;

        Vector3 offsetVec = Vector3.zero;
        if (axis == Axis.x)
            offsetVec = new Vector3(offset,0,0);
        if (axis == Axis.y)
            offsetVec = new Vector3(0,offset,0);
        if (axis == Axis.z)
            offsetVec = new Vector3(0,0,offset);
        // Quaternion deltaRotation = Quaternion.Euler(offsetVec);
        transform.rotation = transform.rotation * Quaternion.Euler(offsetVec) ;//(rb.rotation * deltaRotation);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (axis == Axis.x)
    //         transform.Rotate(Vector3.right*speed*Time.deltaTime);
    //     if (axis == Axis.y)
    //         transform.Rotate(Vector3.up*speed*Time.deltaTime);
    //     if (axis == Axis.z)
    //         transform.Rotate(Vector3.forward*speed*Time.deltaTime);
    // }

    void FixedUpdate()
    {
        if (type == Type.Transform)
        {
            if (axis == Axis.x)
                transform.Rotate(Vector3.right*speed*Time.fixedDeltaTime);
            if (axis == Axis.y)
                transform.Rotate(Vector3.up*speed*Time.fixedDeltaTime);
            if (axis == Axis.z)
                transform.Rotate(Vector3.forward*speed*Time.fixedDeltaTime);
        }
        else //Physics approach
        {
            Vector3 m_EulerAngleVelocity = Vector3.zero;
            if (axis == Axis.x)
                m_EulerAngleVelocity = new Vector3(1,0,0);
            if (axis == Axis.y)
                m_EulerAngleVelocity = new Vector3(0,1,0);
            if (axis == Axis.z)
                m_EulerAngleVelocity = new Vector3(0,0,1);

            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime * speed);
            rb.MoveRotation(rb.rotation * deltaRotation);        
        }
    }
}
