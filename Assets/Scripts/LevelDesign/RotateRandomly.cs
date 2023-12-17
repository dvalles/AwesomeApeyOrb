using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotate and object randomly

public class RotateRandomly : MonoBehaviour
{
    public enum Axis {X,Y,Z};

    //internal
    Rigidbody rb;

    [Header("Params")]
    public float baseSpeed = 1f;
    public float speedRange = 10;
    public float rotChangeSpeed = 1f;
    public Axis axis = Axis.Y;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //a bad way to get a new rotation
        // Quaternion rot = transform.rotation;
        // transform.Rotate(new Vector3(0, 1*speed, 0));
        // Quaternion newRot = transform.rotation;
        // transform.rotation = rot;
        // Debug.Log(rot);
        // Debug.Log(newRot);
        // // transform.rotation = newRot;
        // rb.MoveRotation(newRot);
        Vector3 m_EulerAngleVelocity = new Vector3(0, baseSpeed + Noise.NoiseFBM1(Time.time*rotChangeSpeed, 1f, 3, .5f, 0f)*speedRange, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);        
    }
}
