using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostFinishUI : MonoBehaviour
{
    public static Action PostFinishUIShown;
    public static Action PostFinishUIHidden;

    [Header("Needed Pages")]
    public Canvas loaderCanv;
    public Canvas leaderboardCanv;

    [Header("Needed Elements")]
    public Button retryButton;
    public Button nextButton;
    public TextMeshProUGUI currTimeText;
    public TextMeshProUGUI bestTimeText;
    public Ranking[] rankLineItems;

    private Transform ballCenter;

    void OnEnable()
    {
        FinishLine.OnFinished += LevelFinished;
        SpawnPlayer.OnRespawn += LevelRestarted;
        LevelClock.ClockedTime += TimeClocked;
        SceneManager.sceneLoaded += SceneLoaded;
    }
    
    void OnDisable()
    {
        FinishLine.OnFinished -= LevelFinished;
        SpawnPlayer.OnRespawn -= LevelRestarted;
        LevelClock.ClockedTime -= TimeClocked;
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    void Start()
    {
        ballCenter = GameObject.FindGameObjectWithTag("BallCenter").transform;
        GetComponent<Canvas>().worldCamera = Player.head.GetComponent<Camera>();
        retryButton.onClick.AddListener(() => Player.player.GetComponent<SpawnPlayer>().Spawn());
        nextButton.onClick.AddListener(() => {
            SceneLoader.LoadScene(GetNextScene()).WrapErrors();
        });
    }

    void LateUpdate()
    {
        if (shouldShow)
        {
            //follow player
            transform.root.position = ballCenter.position + menuDistance;

            //hotfix
            if(SceneLoader.currSceneName == "3-2")
            {
                transform.root.position = Player.head.position + Player.head.forward*1.8f;
                transform.root.LookAt(Player.head.position);
            }
        }
    }

    //show the menu to the player, follow them
    Vector3 menuDistance;
    bool shouldShow = false;
    void LevelFinished()
    {
        //show the canvas
        shouldShow = true;
        GetComponent<Canvas>().enabled = true;
        menuDistance = (Player.head.position + Vector3.ProjectOnPlane(Player.head.forward, Vector3.up).normalized*1.8f) - ballCenter.position;
        transform.root.position = ballCenter.position + menuDistance;
        transform.root.LookAt(Player.head.transform.position);
        //let people know
        PostFinishUIShown?.Invoke();
        //Save that they've beaten it
        SetLevelBeaten();
    }

    void SetLevelBeaten()
    {
        PlayerPrefs.SetString(SceneLoader.currSceneName+Oculus.Platform.Samples.EntitlementCheck.EntitlementCheck.oculusID, "Done");
    }

    void LevelRestarted()
    {
        shouldShow = false;
        GetComponent<Canvas>().enabled = false;
        PostFinishUIHidden?.Invoke();
    }

    //display their current time, submit it to leaderboards, pull down leaderboard and best time, display
    async void TimeClocked(float time)
    {
        //set current time
        currTimeText.text = time.ToString();
        
        //show loader
        ShowLoader();

        //submit time to leaderboards
        await LeaderboardHandler.SubmitTimeForLevel(PlayerName.name, SceneManager.GetActiveScene().name, time.RoundTo(3));

        //grab top rankings and best time
        RankingResults results = await LeaderboardHandler.GetRankings(PlayerID.id, SceneManager.GetActiveScene().name);

        //set w/ new data and show
        SetData(results);
        ShowLeaderboard();
    }

    string GetNextScene()
    {
        string currScene = SceneManager.GetActiveScene().name;
        int level = Int32.Parse(currScene.Substring(currScene.Length-1));
        int world = Int32.Parse(currScene.Substring(0,1));
        int nextLevel = MyMath.ShiftOverLoop(level, 1, 1, 5);
        if (level > nextLevel)
            world++;
        return $"{world}-{nextLevel}";
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "4-5") //hard code last level
            HideNextButton();
        else
            ShowNextButton();
    }

    void HideNextButton()
    {
        nextButton.gameObject.SetActive(false);
    }

    void ShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }

    //show the loader page
    void ShowLoader()
    {
        leaderboardCanv.Hide();
        loaderCanv.Show();
    }

    //show the leaderboard page
    void ShowLeaderboard()
    {
        loaderCanv.Hide();
        leaderboardCanv.Show();
    }

    //set leaderboard with results
    void SetData(RankingResults results)
    {
        //grab and set leaderboard elements
        for(int x = 0; x < rankLineItems.Length; x++)
        {
            rankLineItems[x].cg.Show();
            if (x < results.ranks.Count) {
                rankLineItems[x].SetData(results.ranks[x]);
            }
            else
                rankLineItems[x].cg.Hide();
        }

        //set their best time
        bestTimeText.text = results.playerRank.time.ToString();
    }
}
