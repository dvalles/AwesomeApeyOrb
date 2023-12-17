using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Holds common information for everyone else to grab about player
 */

public class Player : MonoBehaviour
{
    public static Transform head {get; private set;}
    public static Transform player {get; private set;}
    
    void OnEnable()
    {
        head = GameObject.FindWithTag("MainCamera").transform;
        player = GameObject.FindWithTag("Player").transform;
    }
    
    void OnDisable()
    {
    
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
