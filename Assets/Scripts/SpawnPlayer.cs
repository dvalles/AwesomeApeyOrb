using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Spawns the player at this location when they load level, and when they fall
 * Also plays audio
 */

public class SpawnPlayer : MonoBehaviour
{
    //events
    public static Action OnSpawn;
    public static Action OnRespawn;

    Transform spawn;
    Transform ball;

    void OnEnable()
    {
        FallCheck.OnFall += PlayerFell;
        SceneManager.sceneLoaded += SceneLoaded;
    }
    
    void OnDisable()
    {
        FallCheck.OnFall -= PlayerFell;
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        //position caching
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        Spawn();
    }

    //spawn the player
    public void Spawn()
    {
        spawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        Vector3 spawnPoint = GetSpawnPoint();
        transform.position = spawnPoint;
        ball.localPosition = Vector3.zero;
        transform.LookAt(spawnPoint + spawn.forward);
        OnRespawn?.Invoke();
    }

    //the player has fallen
    async void PlayerFell()
    {
        await UniTask.Delay(2000);
        spawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        ball.position = GetSpawnPoint();
        OnRespawn?.Invoke(); //for anyone who cares
    }

    #region Helpers
    
    Vector3 GetSpawnPoint()
    {
        int mask = 1 << 8;
        RaycastHit hit;
        Physics.Raycast(spawn.position, Vector3.down, out hit, 30, mask);
        return hit.point + Vector3.up*(ball.lossyScale.y/2f);
    }

    #endregion
}
