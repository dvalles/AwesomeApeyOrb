using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Pass data to a rank and let it handle display itself
 */

public class Ranking : MonoBehaviour
{
    //internal
    [HideInInspector]
    public CanvasGroup cg;
    string playerName;
    string time;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void SetData(Rank rank)
    {
        this.playerName = rank.playerName;
        this.time = rank.time.ToString();
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.playerName;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = this.time;
    }
}
