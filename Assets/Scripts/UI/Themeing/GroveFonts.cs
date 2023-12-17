using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class for access to the fonts used in Grove
 */

public class GroveFonts : MonoBehaviour
{
    //Nunito regular
    static TMPro.TMP_FontAsset nunito;
    public static TMPro.TMP_FontAsset Nunito
    {
        get
        {
            if (nunito == null)
                nunito = Resources.Load("Fonts/Nunito/Nunito-Regular SDF") as TMPro.TMP_FontAsset;
            return nunito;
        }
    }

    //Nunito sans
    static TMPro.TMP_FontAsset nunitoSans;
    public static TMPro.TMP_FontAsset NunitoSans
    {
        get
        {
            if (nunitoSans == null)
                nunitoSans = Resources.Load("Fonts/Nunito/NunitoSans-Regular SDF") as TMPro.TMP_FontAsset;
            return nunitoSans;
        }
    }

    //LiberationSans
    static TMPro.TMP_FontAsset liberationSans;
    public static TMPro.TMP_FontAsset LiberationSans
    {
        get
        {
            if (liberationSans == null)
                liberationSans = Resources.Load("Fonts/LiberationSans/LiberationSans SDF") as TMPro.TMP_FontAsset;
            return liberationSans;
        }
    }
}
