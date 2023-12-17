using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Turns off this layout group in build
 */

public class TurnOffLayoutGroupInBuild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // #if !UNITY_EDITOR
        // GetComponent<VerticalLayoutGroup>()?.enabled = false;
        // GetComponent<HorizontalLayoutGroup>()?.enabled = false;
        // #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
