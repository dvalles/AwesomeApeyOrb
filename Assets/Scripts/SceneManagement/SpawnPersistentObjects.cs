using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This will spawn any object you would like to be persistent across scenes only once
 */

public class SpawnPersistentObjects : MonoBehaviour
{
    //Objects to spawn
    public GameObject[] objects;

    static bool hasSpawned = false;
    void Awake()
    {
        if (hasSpawned == true)
        {
            Destroy(this.gameObject);
            return;
        }

        SpawnObjects();
        hasSpawned = true;  
    }

    //Spawn the objects
    void SpawnObjects()
    {
        // Debug.Log("Spawning Persistent Objects");
        for (int x = 0; x < objects.Length; x++)
        {
            GameObject newObj = GameObject.Instantiate(objects[x].gameObject, objects[x].transform.position, objects[x].transform.rotation);
            newObj.name = objects[x].name;
            newObj.AddComponent<DontDestroyOnLoad>();
        }
        Destroy(this.gameObject);
    }
}
