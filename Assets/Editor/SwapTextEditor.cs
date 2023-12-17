using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(SwapText))]
public class SwapTextEditor : Editor
{
    public override void OnInspectorGUI()
    {
    DrawDefaultInspector();
        SwapText script = (SwapText)target;
        if (GUILayout.Button("Swap for text mesh pro"))
            script.SwapForTMPPro();
    }
}
