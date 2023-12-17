using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Just holds an enumeration to colors
 * My naming conventions need some work haha
 */

public class GroveColors
{
    public enum gColors { GreyDark = 9, GreyTrans = 0, GreyLight = 1, GreyVeryLight = 13, WhiteTrans = 2, WhiteSoft = 3, WhitePure = 7, Blue = 4, Red = 5, Green = 6, BlackPure = 11, BlackSoft = 12, Clear = 10, None = 8};
    public enum fontColors { GreyTrans = 0, Grey = 1, WhitePure = 2, WhiteSoft = 3, WhiteDarker = 4, Red = 5, None = 6};
    static Dictionary<gColors, Color> toColor;
    static Dictionary<fontColors, Color> toFontColor;
    static bool initialized;

    #region Accessors
    
    //general colors
    public static Color GreyDark { get { return GetColor(gColors.Red);}}
    public static Color GreyTrans { get { return GetColor(gColors.GreyTrans);}}
    public static Color GreyLight { get { return GetColor(gColors.GreyLight);}}
    public static Color GreyVeryLight { get { return GetColor(gColors.GreyVeryLight);}}
    public static Color WhiteTrans { get { return GetColor(gColors.WhiteTrans);}}
    public static Color WhiteSoft { get { return GetColor(gColors.WhiteSoft);}}
    public static Color WhitePure { get { return GetColor(gColors.WhitePure);}}
    public static Color Blue { get { return GetColor(gColors.Blue);}}
    public static Color Red { get { return GetColor(gColors.Red);}}
    public static Color Green { get { return GetColor(gColors.Green);}}
    public static Color BlackPure { get { return GetColor(gColors.BlackPure);}}
    public static Color BlackSoft { get { return GetColor(gColors.BlackSoft);}}
    public static Color Clear { get { return GetColor(gColors.Clear);}}

    //text colors
    public static Color GreyTransText { get { return GetColor(fontColors.GreyTrans);}}
    public static Color GreyText { get { return GetColor(fontColors.Grey);}}
    public static Color WhitePureText { get { return GetColor(fontColors.WhitePure);}}
    public static Color WhiteSoftText { get { return GetColor(fontColors.WhiteSoft);}}
    public static Color WhiteDarkerText { get { return GetColor(fontColors.WhiteDarker);}}
    public static Color RedText { get { return GetColor(fontColors.Red);}}

    #endregion

    //retrieve the grove color assigned to the enumeration
    public static Color GetColor(gColors color)
    {
        Initialize();
        return toColor[color];
    }

    //retrieve the grove font color assign to the enumeration
    public static Color GetColor(fontColors color)
    {
        Initialize();
        return toFontColor[color];
    }

    //Check if a certain color exists
    public static bool ContainsColor(gColors color) 
    {
        Initialize();
        if (toColor.ContainsKey(color))
            return true;
        return false;
    }

    //Check if a certain font color exists
    public static bool ContainsColor(fontColors color) 
    {
        Initialize();
        if (toFontColor.ContainsKey(color))
            return true;
        return false;
    }

    //Initialize the arrays
    static void Initialize()
    {
        if (initialized)
            return;
        InitToColor();
        InitToFontColor();
        initialized = true;
    }

    private static void InitToColor()
    {
        toColor = new Dictionary<gColors, Color>();
        toColor[gColors.GreyDark] = new Color32(99, 99, 99, 255);
        toColor[gColors.GreyTrans] = new Color32(130, 130, 130, 221);
        toColor[gColors.GreyLight] = new Color32(171, 173, 165, 255);
        toColor[gColors.GreyVeryLight] = new Color32(185, 185, 185, 255);
        toColor[gColors.WhiteTrans] = new Color32(245, 245, 245, 184);
        toColor[gColors.WhiteSoft] = new Color32(245, 245, 245, 255);
        toColor[gColors.WhitePure] = new Color32(255, 255, 255, 255);
        toColor[gColors.Blue] = new Color32(67, 109, 219, 255);
        // toColor[gColors.Blue] = new Color32(31, 184, 194, 255); #primary blue from mobile, doesn't look so great here though
        // toColor[gColors.Red] = new Color32(221, 41, 41, 255);
        toColor[gColors.Red] = new Color32(227, 65, 50, 255);
        toColor[gColors.Green] = new Color32(132, 156, 93, 255);
        toColor[gColors.Clear] = new Color32(0, 0, 0, 0);
        toColor[gColors.BlackSoft] = new Color32(10, 10, 10, 255);
        toColor[gColors.BlackPure] = new Color32(0, 0, 0, 255);
    }

    private static void InitToFontColor()
    {
        toFontColor = new Dictionary<fontColors, Color>();
        toFontColor[fontColors.GreyTrans] = new Color32(130, 130, 130, 221);
        toFontColor[fontColors.Grey] = new Color32(130, 130, 130, 255);
        toFontColor[fontColors.WhitePure] = new Color32(255, 255, 255, 255);
        toFontColor[fontColors.WhiteSoft] = new Color32(245, 245, 245, 255);
        toFontColor[fontColors.WhiteDarker] = new Color32(235, 235, 235, 255);
        toFontColor[fontColors.Red] = new Color32(221, 41, 41, 255);
    }

}
