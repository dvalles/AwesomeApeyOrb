using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Sets the color of a text mesh pro text
 * Its done this way so I can change color themes at whim and also get dynamic draw batching on elements
 * Uses Grove colors
 */


// [RequireComponent(typeof(TextMeshProUGUI))]
public class SetColorTMP : MonoBehaviour
{
    // public TextMeshProUGUI text;
    public GroveColors.fontColors color;

    //set the color
    public void SetColor(GroveColors.fontColors newColor)
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.color = GroveColors.GetColor(newColor);
    }

    public void OnValidate()
    {
        SetColor(color);
    }
}
