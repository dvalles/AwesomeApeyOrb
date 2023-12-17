using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Moves the lava sound effect under the player
 */

public class LavaSoundEffect : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.head;
        transform.position = player.position - Vector3.up*10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position - Vector3.up*10f;
    }
}
