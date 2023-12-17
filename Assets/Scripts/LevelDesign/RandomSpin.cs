using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spin an object randomly
 */

public class RandomSpin : MonoBehaviour
{
    [Header("Parameters")]
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Noise.NoiseFBM1(Time.time, 1, 6, .5f, 0);
        float y = Noise.NoiseFBM1(Time.time + 50f, 1, 4, .5f, 0);
        float z = Noise.NoiseFBM1(Time.time + 100f, 1, 5, .5f, 0);
        Vector3 angle = new Vector3(x*xSpeed,y*ySpeed,z*zSpeed);
        Quaternion rot = Quaternion.Euler(angle);
        rb.MoveRotation(rb.rotation * rot);
    }
}
