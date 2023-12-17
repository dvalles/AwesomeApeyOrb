using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Models a DB rank object
 */

public class Rank : MonoBehaviour
{
    public string playerName;
    public float time;

    //rank from json
    public Rank(JSONObject json)
    {
        playerName = json.GetField("name").str;
        time = json.GetField("time").f;
    }
}
