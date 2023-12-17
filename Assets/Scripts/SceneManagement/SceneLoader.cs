using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEditor;
using Cysharp.Threading.Tasks;
using System;

/*
 * Loads scenes with a loading screen inbetween and a fade in fade out anim
 * Essentially a wrapper for SceneManagement so I can throw in extra functionality
 */

public class SceneLoader : MonoBehaviour {

    public static Action OnSceneLoadBegin;
    public static Action OnEnteredLoadingScreen;
    public static Action OnExitedLoadingScreen;
    public static Action<Scene, LoadSceneMode> sceneLoaded;

    //Scene names
    const string m_LoadingScreen = "LoadingScreen";

    #region Accessors

    public static Scene currScene { get {return SceneManager.GetActiveScene();} }
    public static string currSceneName { get {return SceneManager.GetActiveScene().name;} }

    static bool InLoadingScene {
        get {
            return currSceneName == m_LoadingScreen;
        }
    }

    #endregion

    #region Setup
    
    static bool setup = false;
    static void SetUp()
    {
        if (setup)
            return;
        setup = true;

        //In case scenes need specific setup
        SceneManager.sceneLoaded += RouteInit;

        LoadObjects();
    }

    //Load any objects used here
    static PlayerVision m_PlayerVision;
    static void LoadObjects()
    {
        GameObject pv = Instantiate(Resources.Load("SceneManagement/VisionBlocker") as GameObject);
        m_PlayerVision = pv.GetComponent<PlayerVision>();
    }

    #endregion

    #region Loaders


    private static async UniTask LoadLoadingScreenAsync()
    {
        if (InLoadingScene)
            return;

        await SceneManager.LoadSceneAsync(m_LoadingScreen);
    }

    public static async UniTask LoadScene(string sceneName, bool useLoadingScreen = true, float fakeLoadTime = 0f)
    {
        SetUp(); //will happen once

        if (currSceneName == sceneName)
            return;

        OnSceneLoadBegin?.Invoke();

        await m_PlayerVision.FadeOut();

        if (useLoadingScreen)
        {
            await LoadLoadingScreenAsync(); 
            OnEnteredLoadingScreen?.Invoke();
        }

        await m_PlayerVision.FadeIn();

        //start load
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        //maybe fake load time
        float startTime = Time.time;
        if (fakeLoadTime != 0f)
        while (Time.time < startTime + fakeLoadTime)
            await UniTask.NextFrame();

        await m_PlayerVision.FadeOut();

        //finish load
        op.allowSceneActivation = true;
        await op;
        OnExitedLoadingScreen?.Invoke();

        await m_PlayerVision.FadeIn();

        return;
    }

    #endregion

    #region Initers
        
    //init the correct scene
    static void RouteInit(Scene scene, LoadSceneMode mode)
    {
        //init
        InitScene(currSceneName);
        //let people know
        sceneLoaded?.Invoke(scene, mode);
    }

    static void InitScene(string sceneName)
    {
        //branch based on scene name
    }

    #endregion

    #region Helpers

    //check if you're currently in a scene by name
    static bool InScene(string sceneName)
    {   
        if (currSceneName == sceneName)
            return true;
        return false;
    }

    #endregion
}