using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/*
 * This script handles the movement of the rolly ball
 * It sources this movement from the players joysticks
 */

public class BallMovement : MonoBehaviour
{
    //internal
    private Rigidbody rb;

    [Header("Needed Objects")]
    public Joystick rightJoystick;
    public Joystick leftJoystick;

    [Header("Params")]
    public float acceleration;
    public float accInAir;
    // public float maxVel;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = Mathf.Infinity;

        rightJoystick.OnJoyStickDir += JoystickDirRecieved;
        leftJoystick.OnJoyStickDir += JoystickDirRecieved;
        SpawnPlayer.OnRespawn += OnRespawn;
        FinishLine.OnFinished += OnFinished;
        SoundOnFinish.OnCheeringDone += CheeringDone;
        SceneLoader.OnEnteredLoadingScreen += EnteredLoadingScreen;
        SceneLoader.OnExitedLoadingScreen += ExitedLoadingScreen;
    }
    
    void OnDisable()
    {
        rightJoystick.OnJoyStickDir -= JoystickDirRecieved;
        leftJoystick.OnJoyStickDir -= JoystickDirRecieved;
        SpawnPlayer.OnRespawn -= OnRespawn;
        FinishLine.OnFinished -= OnFinished;
        SoundOnFinish.OnCheeringDone -= CheeringDone;
        SceneLoader.OnEnteredLoadingScreen -= EnteredLoadingScreen;
        SceneLoader.OnExitedLoadingScreen -= ExitedLoadingScreen;
    }

    void JoystickDirRecieved(Vector3 dir)
    {
        if (!canMove)
            return;

        //check if on ground
        int mask = 1 << 8;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, transform.lossyScale.y/2f + 0.1f, layerMask: mask);

        //get correct torque direction
        Vector3 torqueDir = Vector3.Cross(Vector3.up, dir);

        //add force depending if on ground or not
        if (isGrounded)
            rb.AddTorque(torqueDir*acceleration, ForceMode.Acceleration);
        else
            rb.AddForce(dir*accInAir, ForceMode.Acceleration);
    }

    bool canMove = true;
    void OnRespawn()
    {
        m_RespawnedAfterFinished = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        canMove = true;
        //this is terrible, just a hotfix
        if (SceneLoader.currSceneName == "InitialLoadingScreen") {
            rb.useGravity = false;
        }
    }

    //level finished
    bool m_RespawnedAfterFinished = false;
    async void OnFinished()
    {
        m_RespawnedAfterFinished = false;
        rb.useGravity = false;
        canMove = false;
        while(!m_RespawnedAfterFinished)
        {
            rb.velocity = rb.velocity*.99f;
            rb.angularVelocity = rb.angularVelocity*.99f;
            await UniTask.Yield();
        }
    }

    void EnteredLoadingScreen()
    {
        m_RespawnedAfterFinished = true;
        rb.useGravity = false;
    }

    void ExitedLoadingScreen()
    {
        rb.useGravity = true;
    }

    //cheering is done
    // bool flyingUp = false;
    async void CheeringDone()
    {
        // flyingUp = true;
        await UniTask.Yield();
        // float time = 0;
        // rb.velocity = Vector3.up*30f;
        // while (time < 5f)
        // {
        //     rb.velocity = rb.velocity*1.01f;
        //     time += Time.deltaTime;
        //     await UniTask.Yield();
        // }

        // flyingUp = false;
    }
}
