using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class that maps the text element type to a font, allowing us to change fonts in the future without hassle
 * For now everything is just nunito
 */

// [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class SetFontByType : MonoBehaviour
{
    public enum TextType {Header, Body, Legacy}; //Add new element types here as they come
    public TextType type;

    //set the font based on element type
    public void OnValidate()
    {
        if (type == TextType.Header)
            GetComponent<TMPro.TextMeshProUGUI>().font = GroveFonts.Nunito;
        if (type == TextType.Body)
            GetComponent<TMPro.TextMeshProUGUI>().font = GroveFonts.Nunito;
        if (type == TextType.Legacy)
            GetComponent<TMPro.TextMeshProUGUI>().font = GroveFonts.LiberationSans;
    }
}
