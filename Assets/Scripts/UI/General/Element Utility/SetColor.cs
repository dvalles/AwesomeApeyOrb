using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * Sets the color of the image of this canvas element
 * Its done this way so I can change color themes at whim and also get dynamic draw batching on elements
 * Uses Grove colors
 */

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class SetColor : MonoBehaviour
{
    public GroveColors.gColors color;

    public void OnValidate()
    {
        GetComponent<UnityEngine.UI.Image>().color = GroveColors.GetColor(color);
    }
}
