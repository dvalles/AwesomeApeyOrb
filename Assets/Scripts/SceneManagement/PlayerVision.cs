using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/*
 * Fades the player's vision in and out.
 * Looks for MainCamera
 */

public class PlayerVision : MonoBehaviour
{
    Transform m_Player;
    Material material;

    //Fade their vision out
    public async UniTask FadeOut(float duration = .5f)
    {
        //Grab player
        m_Player = GetPlayer();
        if (m_Player == null)
            return;

        //Position blocker    
        PositionBlocker();

        //Change Opacity
        await FadeInBlocker(duration);
        return;
    }

    private async UniTask FadeInBlocker(float duration = .5f)
    {
        if (material == null)
            material = GetComponentInChildren<Renderer>().sharedMaterial;

        float time = 0;
        while (time <= duration)
        {
            material.color = new Vector4(0f, 0f, 0f, time/duration);
            time += Time.deltaTime;
            await UniTask.NextFrame();
        }
        material.color = new Vector4(0f, 0f, 0f, 1f);
        return;
    }

    //Fade their vision in
    public async UniTask FadeIn(float duration = .5f)
    {
        //Grab player
        m_Player = GetPlayer();
        if (m_Player == null)
            return;

        //Place on their face
        PositionBlocker();

        //Change Opacity
        await FadeBlocker(duration);
        return;
    }

    private async UniTask FadeBlocker(float duration)
    {
        if (material == null)
            material = GetComponentInChildren<Renderer>().sharedMaterial;

        float time = 0;
        while (time <= duration)
        {
            material.color = new Vector4(0f, 0f, 0f, 1f - time/duration);
            time += Time.deltaTime;
            await UniTask.NextFrame();
        }
        material.color = new Vector4(0f, 0f, 0f, 0f);
        return;
    }

    #region Helpers
    
    Transform GetPlayer()
    {
        if (m_Player != null)
            return m_Player;
        return GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    bool hasPositioned = false;
    void PositionBlocker()
    {
        if (hasPositioned)
            return;
        hasPositioned = true;

        //Place on their face
        transform.position = m_Player.position + m_Player.forward*(m_Player.GetComponent<Camera>().nearClipPlane + .001f);
        transform.LookAt(m_Player.position);
        transform.parent = m_Player;
    }

    #endregion
}
