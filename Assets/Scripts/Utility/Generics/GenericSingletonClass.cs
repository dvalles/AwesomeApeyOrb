using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Makes it easy for other classes to be singleton
 */

public class GenericSingletonClass<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance {
      get {
        if (instance == null) {
           instance = FindObjectOfType<T> ();
           if (instance == null) {
             GameObject obj = new GameObject ();
             obj.name = typeof(T).Name;
             instance = obj.AddComponent<T>();
           }
        }
       return instance;
      }
    }
 
    public virtual void Awake ()
    {
       if (instance == null) {
         instance = this as T;
         DontDestroyOnLoad (this.gameObject);
    } else {
      Destroy (gameObject);
    }
  }
}