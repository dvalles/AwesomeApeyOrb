using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("Params")]
    public float distance;
    public float speed;

    //internal
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time*speed); //-> -1 -> 1
        Vector3 angle = new Vector3(0,0,x*distance);
        Quaternion deltaRotation = Quaternion.Euler(angle);
        rb.MoveRotation(rb.rotation * deltaRotation);        
    }
}
