using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/*
 * Shows them a few messages about the discord when the game starts
 */

public class PlugDiscordMenu : MonoBehaviour
{
    public Action OnDone;

    public float fadeTime = 1f;
    public float showTime = 3f;
    public float inBetweenTime = 1f;

    public CanvasGroup firstMessage;
    public CanvasGroup secondMessage;
    public CanvasGroup thirdMessage;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Player.head.position + Player.head.forward*2f;
        transform.LookAt(Player.head.position);

        KickOffPlug();
    }

    async void KickOffPlug()
    {
        #if !UNITY_EDITOR
        await UniTask.Delay(TimeSpan.FromSeconds(inBetweenTime));
        await FadeInOut(firstMessage);
        await UniTask.Delay(TimeSpan.FromSeconds(inBetweenTime));
        await FadeInOut(secondMessage);
        await UniTask.Delay(TimeSpan.FromSeconds(inBetweenTime));
        await FadeInOut(thirdMessage);
        await UniTask.Delay(TimeSpan.FromSeconds(inBetweenTime));
        #endif
        OnDone?.Invoke();
    }

    async UniTask FadeInOut(CanvasGroup cg)
    {
        if (!Application.isPlaying)
            return;
        await cg.FadeIn(fadeTime);
        await UniTask.Delay(TimeSpan.FromSeconds(showTime));
        await cg.Fade(fadeTime);
    }
}
