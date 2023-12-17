using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script moves an object randomly using physics along the xz plane
 */

public class MoveRandomly : MonoBehaviour
{
    //internal
    Rigidbody rb;

    [Header("Params")]
    public float speed = 1f;
    public float height = 1f;
    public float width = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position = new Vector3((Noise.Noise1D(Time.time*speed) * width)-width/2f,transform.position.y, (Noise.Noise1D((Time.time+50)*speed) * height)-height/2f);
        rb.MovePosition(new Vector3((Noise.Noise1D(Time.time*speed) * width)-width/2f,transform.position.y, (Noise.Noise1D((Time.time+50)*speed) * height)-height/2f));
    }
}
