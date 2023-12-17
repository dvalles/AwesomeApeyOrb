using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

/*
 * Helper class to grow or shrink a UI element so users say 'ohhhh fancyyyyy'
 * Works on the UI parent rect transform
 */

public class GrowShrinkElement : MonoBehaviour
{   
    //for anyone who cares
    public delegate void Done(GrowShrinkElement element);
    public event Done OnDone;

    [Tooltip("The duration of the grow or shrink transition")]
    public float duration = 1f;
    [Tooltip("Use smoothing on the transtion?")]
    public bool smoothing = true;

    //internal
    private delegate float Lerp(float a, float b, float t);
    private float postDoneWait = .7f; //how long to wait after grow or shrink before firing event
    RectTransform trans;


    //Grow the element
    public void Grow()
    {
        GrowAsync().WrapErrors();
        // trans = GetComponent<RectTransform>();
        // StartCoroutine(IGrow());
    }

    public async UniTask GrowAsync()
    {
        trans = GetComponent<RectTransform>();
        //set delegate
        Lerp lerp;
        if (smoothing)
            lerp = Mathf.SmoothStep;
        else
            lerp = Mathf.Lerp;
        
        //set scale
        trans.localScale = new Vector3(0,0,1);
        float t = 0;

        // var frameWait = UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        while (t < 1f)
        {
            float newVal = lerp(0,1,t);
            Vector3 newScale = new Vector3(newVal, newVal, 1);
            trans.localScale = newScale;
            t += Time.deltaTime/duration;
            await UniTask.NextFrame();
        }
        //rubber band
        trans.localScale = Vector3.one;
        //Event
        await UniTask.Delay(System.TimeSpan.FromSeconds(postDoneWait));
        OnDone?.Invoke(this);
    }

    // public async Task GrowAsync()
    // {
    //     trans = GetComponent<RectTransform>();
    //     await IGrow();
    // } 

    // IEnumerator IGrow()
    // {
    //     //set delegate
    //     Lerp lerp;
    //     if (smoothing)
    //         lerp = Mathf.SmoothStep;
    //     else
    //         lerp = Mathf.Lerp;
        
    //     //set scale
    //     trans.localScale = new Vector3(0,0,1);
    //     float t = 0;
    //     while (t < 1f)
    //     {
    //         float newVal = lerp(0,1,t);
    //         Vector3 newScale = new Vector3(newVal, newVal, 1);
    //         trans.localScale = newScale;
    //         t += Time.deltaTime/duration;
    //         yield return new WaitForEndOfFrame();
    //     }
    //     //rubber band
    //     trans.localScale = Vector3.one;
    //     //Event
    //     yield return new WaitForSeconds(postDoneWait);
    //     OnDone?.Invoke(this);
    // }

    //Shrink the element
    public void Shrink()
    {
        // trans = GetComponent<RectTransform>();
        // StartCoroutine(IShrink());
        ShrinkAsync();
    } 

    public async UniTask ShrinkAsync()
    {
        trans = GetComponent<RectTransform>();
        Lerp lerp;
        if (smoothing)
            lerp = Mathf.SmoothStep;
        else
            lerp = Mathf.Lerp;
        
        //set scale
        trans.localScale = new Vector3(1,1,1);
        // var frameWait = UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        float t = 0;
        while (t < 1f)
        {
            float newVal = lerp(0,1,1-t);
            Vector3 newScale = new Vector3(newVal, newVal, 1);
            trans.localScale = newScale;
            t += Time.deltaTime/duration;
            await UniTask.NextFrame();
        }
        //rubber band
        trans.localScale = Vector3.zero;
        //Event
        await UniTask.Delay(System.TimeSpan.FromSeconds(postDoneWait));
        OnDone?.Invoke(this);
    }

    // IEnumerator IShrink()
    // {
    //     //set delegate
    //     Lerp lerp;
    //     if (smoothing)
    //         lerp = Mathf.SmoothStep;
    //     else
    //         lerp = Mathf.Lerp;
        
    //     //set scale
    //     trans.localScale = new Vector3(1,1,1);
    //     float t = 0;
    //     while (t < 1f)
    //     {
    //         float newVal = lerp(0,1,1-t);
    //         Vector3 newScale = new Vector3(newVal, newVal, 1);
    //         trans.localScale = newScale;
    //         t += Time.deltaTime/duration;
    //         yield return new WaitForEndOfFrame();
    //     }
    //     //rubber band
    //     trans.localScale = Vector3.zero;
    //     //Event
    //     yield return new WaitForSeconds(postDoneWait);
    //     OnDone?.Invoke(this);
    // }

}
