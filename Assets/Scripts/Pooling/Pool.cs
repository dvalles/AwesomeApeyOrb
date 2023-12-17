using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Generic class for creating a pool of components accessible globally
 */

public abstract class ComponentPool<T> where T : Component
{
    protected static ComponentPool<T> _instance;
    string className;
    Transform parent; //for scene heirarchy
    List<T> members;

    bool initialized = false;
    public void Init()
    {
        if (initialized)
            return;
        _instance = this;
        members = new List<T>();
        className = typeof(T).ToString().Replace("UnityEngine.", "");
        parent = (new GameObject($"{className}Pool")).transform;
        initialized = true;
    }

    //return an instance from pool
    public T Allocate()
    {
        //lazy init
        Init();

        //run through and check if they're in use
        for (int x = 0; x < members.Count; x++)
        {
            if (!InUse(members[x]))
                return members[x];
        }

        //all were in use, create one, setup, and return;
        GameObject holder = new GameObject($"{className}{members.Count}");
        T comp = holder.AddComponent<T>();
        holder.transform.parent = parent;
        SetUp(comp);
        members.Add(comp);
        return comp;
    }

    //check if the member is in use
    public abstract bool InUse(T comp);

    //in case the component has some sort of setup
    public abstract void SetUp(T comp);
}
