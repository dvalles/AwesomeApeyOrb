using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The menu at the start of the game
 */

public class StartSceneMenu : MonoBehaviour
{
    public SetUsernameUI setUsernameUI;

    void OnEnable()
    {
        setUsernameUI.SetUsernameMenuShown += UsernameUIShown;
        setUsernameUI.SetUsernameMenuHidden += UsernameUIHidden;
    }
    
    void OnDisable()
    {
        setUsernameUI.SetUsernameMenuShown -= UsernameUIShown;
        setUsernameUI.SetUsernameMenuHidden -= UsernameUIHidden;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Player.head.GetComponent<Camera>();
    }

    void UsernameUIShown()
    {
        transform.GetChild(0).GetComponent<Canvas>().Hide();
    }

    void UsernameUIHidden()
    {
        transform.GetChild(0).GetComponent<Canvas>().Show();
    }
}
