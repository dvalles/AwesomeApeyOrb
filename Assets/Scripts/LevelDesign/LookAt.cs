using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject lookTowards;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookTowards.transform.position);        
    }
}
