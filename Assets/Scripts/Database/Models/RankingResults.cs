using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the results from a ranking search

public class RankingResults : MonoBehaviour
{
    public List<Rank> ranks;
    public Rank playerRank;

    public RankingResults(JSONObject json)
    {
        this.ranks = GetRanks(json);
        this.playerRank = GetPlayerRank(json.GetField("player"));
    }

    //grab the top ten off json
    List<Rank> GetRanks(JSONObject json)
    {
        List<Rank> ranks = new List<Rank>();
        foreach (string key in json.keys)
        {
            if (key.Contains("rank"))
                ranks.Add(new Rank(json.GetField(key)));
        }
        return ranks;
    }

    //grab player rank off json
    Rank GetPlayerRank(JSONObject json)
    {
        return new Rank(json);
    }
}
