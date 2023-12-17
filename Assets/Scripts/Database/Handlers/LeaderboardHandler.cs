using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LeaderboardHandler : MonoBehaviour
{
    #if UNITY_EDITOR
    static bool useLocal = false; //are you testing currently?
    #else
    static bool useLocal = false;
    #endif

    public static async UniTask SubmitTimeForLevel(string name, string level, float time)
    {
        string path = useLocal ? "http://localhost:5000/awesomeapeyorb/us-central1/leaderboardFunctions/newTime" :
        "https://us-central1-awesomeapeyorb.cloudfunctions.net/leaderboardFunctions/newTime";

        string query = $"?name={name}&time={time}&level={level}&playerID={PlayerID.id}";

        WWWForm form = new WWWForm();
        form.AddField(Password.field, Password.pass);

        UnityWebRequest www = UnityWebRequest.Post(path + query, form);

        try {await www.SendWebRequest();} catch {} //unitask v2 throws errors

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.downloadHandler.text);
            return;// new Response(true, www.downloadHandler.text);
        }
        else
        {
            return;
            // Debug.Log("Set leaderboard: " + www.downloadHandler.text);
            // return new Response(false, www.downloadHandler.text);
        }
    }

    public static async UniTask<RankingResults> GetRankings(string playerID, string level)
    {
        string path = useLocal ? "http://localhost:5000/awesomeapeyorb/us-central1/leaderboardFunctions/getRankings" :
        "https://us-central1-awesomeapeyorb.cloudfunctions.net/leaderboardFunctions/getRankings";

        string query = $"?&level={level}&playerID={PlayerID.id}";

        WWWForm form = new WWWForm();
        form.AddField(Password.field, Password.pass);

        UnityWebRequest www = UnityWebRequest.Post(path + query, form);

        try {await www.SendWebRequest();} catch {} //unitask v2 throws errors

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.downloadHandler.text);
            return null;
        }
        else
        {
            JSONObject json = new JSONObject(www.downloadHandler.text);
            RankingResults results = new RankingResults(json);
            return results;
        }
    }
}
