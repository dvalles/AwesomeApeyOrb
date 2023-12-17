using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sets the buttons onClick
 */

public class LevelSelectButton : MonoBehaviour
{
    public string levelName;
    private UnityEngine.UI.Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() => SceneLoader.LoadScene(levelName));
        #if !UNITY_EDITOR
        HideIfNotBeatenPrevious();
        #endif
    }

    void HideIfNotBeatenPrevious()
    {
        if (levelName == "1-1") //always show first
            button.interactable = true;
        else
        {
            int world = Int32.Parse(levelName.Substring(0,1)); // get world
            int level = Int32.Parse(levelName.Substring(levelName.Length-1)); //get level
            int previousLevel = MyMath.ShiftOverLoop(level, -1, 1, 5);
            if (previousLevel > level)
                world--;
            string toCheck = $"{world}-{previousLevel}"+Oculus.Platform.Samples.EntitlementCheck.EntitlementCheck.oculusID;
            if (PlayerPrefs.GetString(toCheck) == "") //they haven't beaten the previous
                button.interactable = false;
            else
                button.interactable = true;
        }

    }
}
