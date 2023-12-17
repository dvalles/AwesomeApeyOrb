using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwapText : MonoBehaviour
{
    public void SwapForTMPPro()
    {
        foreach (Text item in gameObject.GetComponentsInChildren<Text>())
        {
            Swap(item.gameObject);
        }
    }

    public void Swap(GameObject gameObject)
    {
        string text = gameObject.GetComponent<Text>().text;
        int fontSize = gameObject.GetComponent<Text>().fontSize;
        DestroyImmediate(gameObject.GetComponent<Text>());
        gameObject.AddComponent<TextMeshProUGUI>();
        gameObject.GetComponent<TextMeshProUGUI>().text = text;
        gameObject.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
        gameObject.GetComponent<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;
        gameObject.GetComponent<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Center;
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(.5f, .5f, .5f, 1f);    
        DestroyImmediate(gameObject.GetComponent<SwapText>());
    }
}
