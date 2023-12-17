using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirParticleEffect : MonoBehaviour
{
    ParticleSystem effect;

    void OnEnable()
    {
        // effect = GetComponent<ParticleSystem>();
        // SceneLoader.OnEnteredLoadingScreen += EnteredLoadingScreen;
        // SceneLoader.OnExitedLoadingScreen += ExitedLoadingScreen;
    }
    
    void OnDisable()
    {
        // SceneLoader.OnEnteredLoadingScreen -= EnteredLoadingScreen;
        // SceneLoader.OnExitedLoadingScreen -= ExitedLoadingScreen;
    }

    void EnteredLoadingScreen()
    {
        // effect.Stop();
    }

    void ExitedLoadingScreen()
    {
        // effect.Play();
    }

    void LateUpdate()
    {
        if (Player.head != null)
            transform.position = Player.head.position;
    }
}
