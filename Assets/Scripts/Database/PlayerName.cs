using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName
{
    public readonly static string name_field = "username";

    public static string name {
        get
        {
            return GetPlayerName();
        }
    }

    //check if they have an ID, if so return it, if not, set and return it
    static string GetPlayerName()
    {
        string playerName = PlayerPrefs.GetString(name_field+Oculus.Platform.Samples.EntitlementCheck.EntitlementCheck.oculusID);
        return playerName;
    }

    public static void SetName(string name)
    {
        PlayerPrefs.SetString(PlayerName.name_field+Oculus.Platform.Samples.EntitlementCheck.EntitlementCheck.oculusID, name);
    }
}
