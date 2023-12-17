using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Threading;
using System.Linq;
using Cysharp.Threading.Tasks;

public static class ExtensionMethods
{

    #region Transform

    public static T[] GetComponentsInImmediateChildren<T>(this Transform trans)
    {
        List<T> comps = new List<T>();
        for (int x = 0; x < trans.childCount; x++)
        {
            Transform child = trans.GetChild(x);
            T comp = child.GetComponent<T>();
            if (comp != null)
                comps.Add(comp);
        }
        return comps.ToArray();
    }

    #endregion

    #region Array

    //I'd like to figure out how to make this void   
    public static T[] RemoveElements<T>(this T[] array, T[] elements)
    {
        if (elements.Length == 0 || array.Length == 0)
            return array;

        List<T> list = array.ToList();
        foreach (T item in elements)
        {
            list.Remove(item);
        }
        return list.ToArray();
    }

    //get part of an array
    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    //check if an array contains a value
    public static bool Contains<T>(this T[] arr, T data)
    {
        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x].Equals(data))
                return true;
        }
        return false;
    }

    //append an array onto another one
    public static T[] Append<T>(this T[] arr, T[] toAppend)
    {
        T[] newArr = new T[arr.Length + toAppend.Length];
        int x = 0;
        for (; x < arr.Length; x++)
        {
            newArr[x] = arr[x];
        }
        for (; x < arr.Length + toAppend.Length; x++)
        {
            newArr[x] = toAppend[x - arr.Length];
        }
        return newArr;
    }

    #endregion

    #region UniTask

    public static async void WrapErrors(this UniTask task)
    {
        await task;
    }

    // public static async void WrapErrors(this UniTaskVoid task)
    // {
    //     await task;
    // }

    public static async void WrapErrors<T>(this UniTask<T> task)
    {
        await task;
    }

    #endregion

    #region Vector3

    //set just x
    public static void SetX(this Vector3 vec, float x)
    {
        vec = new Vector3(x, vec.y, vec.z);
    }

    //set just y
    public static void SetY(this Vector3 vec, float y)
    {
        vec = new Vector3(vec.x, y, vec.z);
    }

    //set just z
    public static void SetZ(this Vector3 vec, float z)
    {
        vec = new Vector3(vec.x, vec.y, z);
    }

    //cast to vector3Int
    public static Vector3Int ToVector3Int(this Vector2Int vec, int z = 0)
    {
        return new Vector3Int(vec.x, vec.y, z);
    }

    //cast to vector3
    public static Vector3 ToVector3(this Vector2 vec, float z = 0)
    {
        return new Vector3(vec.x, vec.y, z);
    }

    //cast to vector2Int
    public static Vector2Int ToVector2Int(this Vector3Int vec)
    {
        return new Vector2Int(vec.x, vec.y);
    }

    //cast to vector2Int
    public static Vector2Int ToVector2Int(this Vector3 vec)
    {
        return new Vector2Int((int)vec.x, (int)vec.y);
    }

    //cast to vector2
    public static Vector2 ToVector2(this Vector3 vec, int indexToExclude = 2)
    {
        indexToExclude = Mathf.Clamp(indexToExclude, 0, 2);
        if (indexToExclude == 2)
            return new Vector2(vec.x, vec.y);
        if (indexToExclude == 1)
            return new Vector2(vec.x, vec.z);
        if (indexToExclude == 0)
            return new Vector2(vec.y, vec.z);
        return Vector2.one;
    }

    //vector3 to vector4
    public static Vector4 ToVector4(this Vector3 vec, float w = 0)
    {
        return new Vector4(vec.x, vec.y, vec.z, w);
    }

    //vector4 to vector3
    public static Vector3 ToVector3(this Vector4 vec)
    {
        return new Vector3(vec.x, vec.y, vec.z);
    }

    //vector3 to color
    public static Color ToColor(this Vector3 vec)
    {
        return new Color(vec.x, vec.y, vec.z);
    }

    //scale a vector and actually have it work
    public static Vector3 Scale(this Vector3 vec, float x, float y, float z)
    {
        return new Vector3(vec.x * x, vec.y * y, vec.z * z);
    }

    #endregion

    #region Color

    //scale a color and actually have it work
    public static Color Scale(this Color col, float r, float g, float b, float a)
    {
        return new Color(col.r * r, col.g * g, col.b * b, col.a * a);
    }

    //apply gamma conversion to color
    public static Color ApplyGamma(this Color col)
    {
        float exp = 1 / 2.2f;
        return new Color(Mathf.Pow(col.r, exp), Mathf.Pow(col.g, exp), Mathf.Pow(col.b, exp), Mathf.Pow(col.a, exp));
    }

    //apply linear conversion to color
    public static Color ApplyLinear(this Color col)
    {
        float exp = 2.2f;
        return new Color(Mathf.Pow(col.r, exp), Mathf.Pow(col.g, exp), Mathf.Pow(col.b, exp), Mathf.Pow(col.a, exp));
    }

    //Color to vector3
    public static Vector3 ToVector3(this Color col)
    {
        return new Vector3(col.r, col.g, col.b);
    }

    //Color to vector4
    public static Vector4 ToVector4(this Color col, float z = 0)
    {
        return new Vector4(col.r, col.g, col.b, z);
    }

    #endregion

    #region DateTime

    //Give back a nothingness DateTime   
    public static DateTime Zero(this DateTime date)
    {
        return new DateTime(0001, 1, 1, 0, 0, 0);
    }

    #endregion

    #region GameObject

    //Set the layer of all children
    public static void SetLayerRecursively(this GameObject go, int layerNumber)
    {
        Transform[] transComps = go.GetComponentsInChildren<Transform>(true);
        for (int x = 0; x < transComps.Length; x++)
        {
            transComps[x].gameObject.layer = layerNumber;
        }
    }

    //Find a gameobject by name in children
    public static GameObject FindObjectInChildren(this GameObject parent, string name)
    {
        if (parent == null)
            return null;
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    #endregion

    #region String

    //shortens string to maxLength, including dots
    public static string ShortenString(this string str, int maxLength, bool addDots = true)
    {
        if (str.Length > maxLength && addDots)
            return str.Substring(0, maxLength - 3) + "...";
        if (str.Length > maxLength)
            return str.Substring(0, maxLength);
        return str;
    }

    //get the packed size of a string (packed into byte array, length, then string value)
    public static int GetPackedLength(this string str)
    {
        return sizeof(int) + System.Text.Encoding.ASCII.GetByteCount(str);
    }

    #endregion

    #region String[]

    //turn string array into one long string for db
    public static string ToParsableString(this string[] strings)
    {
        if (strings.Length == 0)
            return "[]";
        System.Text.StringBuilder builder = new System.Text.StringBuilder("[");
        for (int x = 0; x < strings.Length; x++)
        {
            builder.Append($"\"{strings[x]}\",");
        }
        builder.Remove(builder.Length - 1, 1); //remove last comma
        builder.Append("]");
        return builder.ToString();
    }

    #endregion

    #region int[]

    //turn int array into one long string for db
    public static string ToParsableString(this int[] ints)
    {
        if (ints.Length == 0)
            return "[]";
        System.Text.StringBuilder builder = new System.Text.StringBuilder("[");
        for (int x = 0; x < ints.Length; x++)
        {
            builder.Append($"{ints[x]},");
        }
        builder.Remove(builder.Length - 1, 1); //remove last comma
        builder.Append("]");
        return builder.ToString();
    }

    #endregion

    #region AudioSource

    //fade audio source out (doesn't quite work correctly with endingVolume)
    public static async void FadeOut(this AudioSource audioSource, float FadeTime, float endingVolume = .01f)
    {
        if (audioSource == null) return; //error check

        while (audioSource.volume > endingVolume)
        {
            audioSource.volume -= Time.deltaTime / FadeTime;
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        }
        audioSource.Stop();
        audioSource.volume = 1f; //reset after out
    }

    //fade audio source in
    public static async void FadeIn(this AudioSource audioSource, float FadeTime, float volume = 1f)
    {
        if (audioSource == null) return; //error check

        if (!audioSource.isPlaying)
            audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < volume)
        {
            audioSource.volume += (Time.deltaTime / FadeTime) * volume;
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        }
    }

    //wait until the audio source is done playing
    public static async UniTask DonePlaying(this AudioSource audioSource)
    {
        while (audioSource != null && audioSource.isPlaying)
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        return;
    }

    #endregion

    #region AudioClip[]

    public static AudioClip RandomClip(this AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length - 1)];
    }

    #endregion

    #region RaycastResults

    //not full proof, but good enough for my application
    public static bool EqualsNoAlloc(this UnityEngine.EventSystems.RaycastResult res1, UnityEngine.EventSystems.RaycastResult res2)
    {
        // if (res1.GetHashCode() == res2.GetHashCode()) //get hash code actually allocates as well
        if (res1.worldPosition == res2.worldPosition && res1.worldNormal == res2.worldNormal && res1.screenPosition == res2.screenPosition)
            return true;
        return false;
    }

    #endregion

    #region Canvas Group

    //Hide canvas group
    public static void Hide(this CanvasGroup canv)
    {
        canv.alpha = 0;
        canv.blocksRaycasts = false;
        canv.interactable = false;
    }

    //Show canvas group
    public static void Show(this CanvasGroup canv)
    {
        canv.alpha = 1;
        canv.blocksRaycasts = true;
        canv.interactable = true;
    }

    //Fade in a canvas group
    public static async UniTask FadeIn(this CanvasGroup cg, float fadeTime)
    {
        if (cg == null)
            return;

        cg.alpha = 0f;
        float time = 0;
        while (time < fadeTime)
        {
            cg.alpha = time / fadeTime;
            time += Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        }
        cg.Show();
    }

    //Fade out a canvas group
    public static async UniTask Fade(this CanvasGroup cg, float fadeTime)
    {
        if (cg == null)
            return;

        cg.alpha = 1f;
        float time = 0;
        while (time < fadeTime)
        {
            cg.alpha = 1f - (time / fadeTime);
            time += Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        }
        cg.Hide();
    }

    #endregion

    #region Canvas

    //Hide canvas
    public static void Hide(this Canvas canv)
    {
        canv.enabled = false;
    }

    //Show canvas
    public static void Show(this Canvas canv)
    {
        canv.enabled = true;
    }

    #endregion

}
