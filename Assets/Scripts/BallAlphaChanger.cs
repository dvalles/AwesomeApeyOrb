using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Changes the alpha of the ball material depending on the world
 */

public class BallAlphaChanger : MonoBehaviour
{
    Material mat;

    void OnEnable()
    {
        mat = GetComponent<Renderer>().sharedMaterial;
        SceneManager.sceneLoaded += SceneLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    //called when scene loaded
    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string name = scene.name;
        if (name.Substring(0,1) == "1") //world 1
            mat.color = new Color(mat.color.r,mat.color.g,mat.color.b, .1894f);
        if (name.Substring(0,1) == "2") //world 2
            mat.color = new Color(mat.color.r,mat.color.g,mat.color.b, .35f);
        if (name.Substring(0,1) == "3") //world 3
            mat.color = new Color(mat.color.r,mat.color.g,mat.color.b, .072f);
        if (name.Substring(0,1) == "4") //world 4
            mat.color = new Color(mat.color.r,mat.color.g,mat.color.b, .0627f);
    }
}
