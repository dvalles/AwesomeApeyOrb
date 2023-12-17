using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Just load the first scene
 */

public class LoadFirstScene : MonoBehaviour
{
    public string firstScene;
    public PlugDiscordMenu plugMenu;

    void OnEnable()
    {
        plugMenu.OnDone += LoadScene;
    }
    
    void OnDisable()
    {
        plugMenu.OnDone -= LoadScene;
    }

    void LoadScene()
    {
        SceneLoader.LoadScene(firstScene, false).WrapErrors();
    }
}
