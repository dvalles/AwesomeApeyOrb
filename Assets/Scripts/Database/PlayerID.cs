using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script creates a unique persistent ID for the player
 */

public class PlayerID : MonoBehaviour
{
    public static string id {
        get
        {
            return GetPlayerID();
        }
    }

    //check if they have an ID, if so return it, if not, set and return it
    static string GetPlayerID()
    {
        string playerID = PlayerPrefs.GetString("id"+Oculus.Platform.Samples.EntitlementCheck.EntitlementCheck.oculusID);
        if (playerID == "")
        {
            string newID = GenerateID();
            PlayerPrefs.SetString("id"+Oculus.Platform.Samples.EntitlementCheck.EntitlementCheck.oculusID, newID);
            playerID = newID;
        }
        return playerID;
    }

    //generate a new id string for the player
    static string GenerateID()
    {
        return Guid.NewGuid().ToString();
    }
}
