using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Don't destroy this object when loading a level
 */

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
