using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;
using Unity.XR.Oculus;

/*
 * A class for XR Controller Input that others can subscribe to
 * Only contains Input related to hand controllers
 */

public class XRControllerInput : MonoBehaviour
{
    #region Actions people can subscribe to

    //BUTTONS ACTIONS

    public static Action leftTriggerPressed;
    public static Action leftThumbTouchPressed;
    public static Action leftPrimaryAxisClickPressed;
    public static Action leftPrimaryAxisTouchPressed;
    public static Action leftMenuButtonPressed;
    public static Action leftGripButtonPressed;
    public static Action leftsecondaryButtonPressed;
    public static Action leftsecondaryButtonTouchPressed;
    public static Action leftprimaryButtonPressed;
    public static Action leftprimaryButtonTouchPressed;
    public static Action leftIndexTouchPressed;

    public static Action leftTriggerHeld;
    public static Action leftThumbTouchHeld;
    public static Action leftPrimaryAxisClickHeld;
    public static Action leftPrimaryAxisTouchHeld;
    public static Action leftMenuButtonHeld;
    public static Action leftGripButtonHeld;
    public static Action leftsecondaryButtonHeld;
    public static Action leftsecondaryButtonTouchHeld;
    public static Action leftprimaryButtonHeld;
    public static Action leftprimaryButtonTouchHeld;
    public static Action leftIndexTouchHeld;

    public static Action leftTriggerReleased;
    public static Action leftThumbTouchReleased;
    public static Action leftPrimaryAxisClickReleased;
    public static Action leftPrimaryAxisTouchReleased;
    public static Action leftMenuButtonReleased;
    public static Action leftGripButtonReleased;
    public static Action leftsecondaryButtonReleased;
    public static Action leftsecondaryButtonTouchReleased;
    public static Action leftprimaryButtonReleased;
    public static Action leftprimaryButtonTouchReleased;
    public static Action leftIndexTouchReleased;

    public static Action rightTriggerPressed;
    public static Action rightThumbTouchPressed;
    public static Action rightPrimaryAxisClickPressed;
    public static Action rightPrimaryAxisTouchPressed;
    public static Action rightMenuButtonPressed;
    public static Action rightGripButtonPressed;
    public static Action rightsecondaryButtonPressed;
    public static Action rightsecondaryButtonTouchPressed;
    public static Action rightprimaryButtonPressed;
    public static Action rightprimaryButtonTouchPressed;
    public static Action rightIndexTouchPressed;

    public static Action rightTriggerHeld;
    public static Action rightThumbTouchHeld;
    public static Action rightPrimaryAxisClickHeld;
    public static Action rightPrimaryAxisTouchHeld;
    public static Action rightMenuButtonHeld;
    public static Action rightGripButtonHeld;
    public static Action rightsecondaryButtonHeld;
    public static Action rightsecondaryButtonTouchHeld;
    public static Action rightprimaryButtonHeld;
    public static Action rightprimaryButtonTouchHeld;
    public static Action rightIndexTouchHeld;

    public static Action rightTriggerReleased;
    public static Action rightThumbTouchReleased;
    public static Action rightPrimaryAxisClickReleased;
    public static Action rightPrimaryAxisTouchReleased;
    public static Action rightMenuButtonReleased;
    public static Action rightGripButtonReleased;
    public static Action rightsecondaryButtonReleased;
    public static Action rightsecondaryButtonTouchReleased;
    public static Action rightprimaryButtonReleased;
    public static Action rightprimaryButtonTouchReleased;
    public static Action rightIndexTouchReleased;

    //AXIS ACTIONS

    public static Action<float> leftTriggerAxis;
    public static Action<float> leftGripAxis;
    public static Action<float> leftBatteryLevelAxis;

    public static Action<float> rightTriggerAxis;
    public static Action<float> rightGripAxis;
    public static Action<float> rightBatteryLevelAxis;

    //AXIS2D

    public static Action<Vector2> leftPrimaryAxis2D;
    public static Action<Vector2> leftSecondaryAxis2D;

    public static Action<Vector2> rightPrimaryAxis2D;
    public static Action<Vector2> rightSecondaryAxis2D;

    #endregion

    //internal
    private List<InputDevice> leftHandDevices;
    private List<InputDevice> rightHandDevices;

    //BUTTON STRUCTURES

    //button currentlyPressed arrays, so I can for loop over them
    bool[] pressedValsLeft; 
    bool[] pressedValsRight;

    //all the buttons we'll check for
    readonly InputFeatureUsage<bool>[] availableButtons = { 
         CommonUsages.triggerButton,
         OculusUsages.thumbTouch,
         CommonUsages.primary2DAxisClick,
         CommonUsages.primary2DAxisTouch,
         CommonUsages.menuButton,
         CommonUsages.gripButton,
         CommonUsages.secondaryButton,
         CommonUsages.secondaryTouch,
         CommonUsages.primaryButton,
         CommonUsages.primaryTouch,
         OculusUsages.indexTouch,
    };

    //action arrays, so I can for loop over them
    Action[] leftPressedActions;
    Action[] leftHeldActions;
    Action[] leftReleasedActions;
    Action[] rightPressedActions;
    Action[] rightHeldActions;
    Action[] rightReleasedActions;

    //AXIS STRUCTURES

    //all the axis' we'll check for
    readonly InputFeatureUsage<float>[] availableAxis = { 
         CommonUsages.trigger,
         CommonUsages.grip,
         CommonUsages.batteryLevel,
    };

    Action<float>[] leftAxisActions;
    Action<float>[] rightAxisActions;

    //AXIS2D STRUCTURES

    //all the axis2Ds we'll check for
    readonly InputFeatureUsage<Vector2>[] availableAxis2D = { 
         CommonUsages.primary2DAxis,
         CommonUsages.secondary2DAxis,
    };

    Action<Vector2>[] leftAxis2DActions;
    Action<Vector2>[] rightAxis2DActions;

    void Awake()
    {
        leftHandDevices = new List<InputDevice>();
        rightHandDevices = new List<InputDevice>();
        pressedValsLeft = new bool[availableButtons.Length];
        pressedValsRight = new bool[availableButtons.Length];
        SetUpActions();
    }

    void OnEnable()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        foreach (InputDevice device in devices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        leftHandDevices.Clear();
        rightHandDevices.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        //if left hand add to left
        if ((device.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left)
            leftHandDevices.Add(device);
        //if right add to right
        if ((device.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right)
            rightHandDevices.Add(device);
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (leftHandDevices.Contains(device))
            leftHandDevices.Remove(device);
        if (rightHandDevices.Contains(device))
            rightHandDevices.Remove(device);
    }

    // Update is called once per frame
    void Update()
    {
        //left hand(s)
        foreach (InputDevice device in leftHandDevices)
        {
            //each button
            for (int x = 0; x < availableButtons.Length; x++)
            {
                //get the button value
                InputFeatureUsage<bool> feature = availableButtons[x];
                bool inputValue = false;
                bool buttonPressed = device.TryGetFeatureValue(feature, out inputValue) && inputValue;
                //broadcast the correct action
                if (buttonPressed)
                {
                    //they weren't pressing previously
                    if (pressedValsLeft[x] == false)
                    {
                        pressedValsLeft[x] = true; //now they are
                        leftPressedActions[x]?.Invoke();
                    }
                    //they were pressing, they're holding the button
                    else
                    {
                        leftHeldActions[x]?.Invoke();
                    }
                }
                // they were pressing, now they're not
                else if (pressedValsLeft[x] == true)
                {
                    pressedValsLeft[x] = false;
                    leftReleasedActions[x]?.Invoke();
                }
            }

            //each axis1D
            for (int x = 0; x < availableAxis.Length; x++)
            {
                InputFeatureUsage<float> feature = availableAxis[x];
                float inputValue = 0f;
                bool buttonAvailable = device.TryGetFeatureValue(feature, out inputValue);
                if (buttonAvailable)
                    leftAxisActions[x]?.Invoke(inputValue);
            }

            //each axis2D
            for (int x = 0; x < availableAxis2D.Length; x++)
            {
                InputFeatureUsage<Vector2> feature = availableAxis2D[x];
                Vector2 inputValue = Vector2.zero;
                bool buttonAvailable = device.TryGetFeatureValue(feature, out inputValue);
                if (buttonAvailable)
                    leftAxis2DActions[x]?.Invoke(inputValue);
            }
        }

        //right hand(s)
        foreach (InputDevice device in rightHandDevices)
        {
            //each button
            for (int x = 0; x < availableButtons.Length; x++)
            {
                //get the button value
                InputFeatureUsage<bool> feature = availableButtons[x];
                bool inputValue = false;
                bool buttonPressed = device.TryGetFeatureValue(feature, out inputValue) && inputValue;

                //broadcast the correct action
                if (buttonPressed)
                {
                    //they weren't pressing previously
                    if (pressedValsRight[x] == false)
                    {
                        pressedValsRight[x] = true; //now they are

                        rightPressedActions[x]?.Invoke();
                    }
                    //they were pressing, they're holding the button
                    else
                    {
                        rightHeldActions[x]?.Invoke();
                    }
                }
                // they were pressing, now they're not
                else if (pressedValsRight[x] == true)
                {
                    pressedValsRight[x] = false;
                    rightReleasedActions[x]?.Invoke();
                }
            }

            //each axis1D
            for (int x = 0; x < availableAxis.Length; x++)
            {
                InputFeatureUsage<float> feature = availableAxis[x];
                float inputValue = 0f;
                bool buttonAvailable = device.TryGetFeatureValue(feature, out inputValue);
                if (buttonAvailable)
                    rightAxisActions[x]?.Invoke(inputValue);
            }

            //each axis2D
            for (int x = 0; x < availableAxis2D.Length; x++)
            {
                InputFeatureUsage<Vector2> feature = availableAxis2D[x];
                Vector2 inputValue = Vector2.zero;
                bool buttonAvailable = device.TryGetFeatureValue(feature, out inputValue);
                if (buttonAvailable)
                    rightAxis2DActions[x]?.Invoke(inputValue);
            }
        }
    }

    #region Helpers
    
    //set up all the actions. There was a problem putting them in an array directly, so you make an array of actions that call the action
    void SetUpActions()
    {
        //left pressed
        leftPressedActions = new Action[availableButtons.Length];
        leftPressedActions[0] += () => {leftTriggerPressed?.Invoke();};
        leftPressedActions[1] += () => {leftThumbTouchPressed?.Invoke();};
        leftPressedActions[2] += () => {leftPrimaryAxisClickPressed?.Invoke();};
        leftPressedActions[3] += () => {leftPrimaryAxisTouchPressed?.Invoke();};
        leftPressedActions[4] += () => {leftMenuButtonPressed?.Invoke();};
        leftPressedActions[5] += () => {leftGripButtonPressed?.Invoke();};
        leftPressedActions[6] += () => {leftsecondaryButtonPressed?.Invoke();};
        leftPressedActions[7] += () => {leftsecondaryButtonTouchPressed?.Invoke();};
        leftPressedActions[8] += () => {leftprimaryButtonPressed?.Invoke();};
        leftPressedActions[9] += () => {leftprimaryButtonTouchPressed?.Invoke();};
        leftPressedActions[10] += () => {leftIndexTouchPressed?.Invoke();};

        //left held
        leftHeldActions = new Action[availableButtons.Length];
        leftHeldActions[0] += () => {leftTriggerHeld?.Invoke();};
        leftHeldActions[1] += () => {leftThumbTouchHeld?.Invoke();};
        leftHeldActions[2] += () => {leftPrimaryAxisClickHeld?.Invoke();};
        leftHeldActions[3] += () => {leftPrimaryAxisTouchHeld?.Invoke();};
        leftHeldActions[4] += () => {leftMenuButtonHeld?.Invoke();};
        leftHeldActions[5] += () => {leftGripButtonHeld?.Invoke();};
        leftHeldActions[6] += () => {leftsecondaryButtonHeld?.Invoke();};
        leftHeldActions[7] += () => {leftsecondaryButtonTouchHeld?.Invoke();};
        leftHeldActions[8] += () => {leftprimaryButtonHeld?.Invoke();};
        leftHeldActions[9] += () => {leftprimaryButtonTouchHeld?.Invoke();};
        leftHeldActions[10] += () => {leftIndexTouchHeld?.Invoke();};

        //left released
        leftReleasedActions = new Action[availableButtons.Length];
        leftReleasedActions[0] += () => {leftTriggerReleased?.Invoke();};
        leftReleasedActions[1] += () => {leftThumbTouchReleased?.Invoke();};
        leftReleasedActions[2] += () => {leftPrimaryAxisClickReleased?.Invoke();};
        leftReleasedActions[3] += () => {leftPrimaryAxisTouchReleased?.Invoke();};
        leftReleasedActions[4] += () => {leftMenuButtonReleased?.Invoke();};
        leftReleasedActions[5] += () => {leftGripButtonReleased?.Invoke();};
        leftReleasedActions[6] += () => {leftsecondaryButtonReleased?.Invoke();};
        leftReleasedActions[7] += () => {leftsecondaryButtonTouchReleased?.Invoke();};
        leftReleasedActions[8] += () => {leftprimaryButtonReleased?.Invoke();};
        leftReleasedActions[9] += () => {leftprimaryButtonTouchReleased?.Invoke();};
        leftReleasedActions[10] += () => {leftIndexTouchReleased?.Invoke();};

        //right pressed
        rightPressedActions = new Action[availableButtons.Length];
        rightPressedActions[0] += () => {rightTriggerPressed?.Invoke();};
        rightPressedActions[1] += () => {rightThumbTouchPressed?.Invoke();};
        rightPressedActions[2] += () => {rightPrimaryAxisClickPressed?.Invoke();};
        rightPressedActions[3] += () => {rightPrimaryAxisTouchPressed?.Invoke();};
        rightPressedActions[4] += () => {rightMenuButtonPressed?.Invoke();};
        rightPressedActions[5] += () => {rightGripButtonPressed?.Invoke();};
        rightPressedActions[6] += () => {rightsecondaryButtonPressed?.Invoke();};
        rightPressedActions[7] += () => {rightsecondaryButtonTouchPressed?.Invoke();};
        rightPressedActions[8] += () => {rightprimaryButtonPressed?.Invoke();};
        rightPressedActions[9] += () => {rightprimaryButtonTouchPressed?.Invoke();};
        rightPressedActions[10] += () => {rightIndexTouchPressed?.Invoke();};


        //right held
        rightHeldActions = new Action[availableButtons.Length];
        rightHeldActions[0] += () => {rightTriggerHeld?.Invoke();};
        rightHeldActions[1] += () => {rightThumbTouchHeld?.Invoke();};
        rightHeldActions[2] += () => {rightPrimaryAxisClickHeld?.Invoke();};
        rightHeldActions[3] += () => {rightPrimaryAxisTouchHeld?.Invoke();};
        rightHeldActions[4] += () => {rightMenuButtonHeld?.Invoke();};
        rightHeldActions[5] += () => {rightGripButtonHeld?.Invoke();};
        rightHeldActions[6] += () => {rightsecondaryButtonHeld?.Invoke();};
        rightHeldActions[7] += () => {rightsecondaryButtonTouchHeld?.Invoke();};
        rightHeldActions[8] += () => {rightprimaryButtonHeld?.Invoke();};
        rightHeldActions[9] += () => {rightprimaryButtonTouchHeld?.Invoke();};
        rightHeldActions[10] += () => {rightIndexTouchHeld?.Invoke();};

        //right released
        rightReleasedActions = new Action[availableButtons.Length];
        rightReleasedActions[0] += () => {rightTriggerReleased?.Invoke();};
        rightReleasedActions[1] += () => {rightThumbTouchReleased?.Invoke();};
        rightReleasedActions[2] += () => {rightPrimaryAxisClickReleased?.Invoke();};
        rightReleasedActions[3] += () => {rightPrimaryAxisTouchReleased?.Invoke();};
        rightReleasedActions[4] += () => {rightMenuButtonReleased?.Invoke();};
        rightReleasedActions[5] += () => {rightGripButtonReleased?.Invoke();};
        rightReleasedActions[6] += () => {rightsecondaryButtonReleased?.Invoke();};
        rightReleasedActions[7] += () => {rightsecondaryButtonTouchReleased?.Invoke();};
        rightReleasedActions[8] += () => {rightprimaryButtonReleased?.Invoke();};
        rightReleasedActions[9] += () => {rightprimaryButtonTouchReleased?.Invoke();};
        rightReleasedActions[10] += () => {rightIndexTouchReleased?.Invoke();};

        //left axis 1D
        leftAxisActions = new Action<float>[availableAxis.Length];
        leftAxisActions[0] += (float x) => {leftTriggerAxis?.Invoke(x);};
        leftAxisActions[1] += (float x) => {leftGripAxis?.Invoke(x);};
        leftAxisActions[2] += (float x) => {leftBatteryLevelAxis?.Invoke(x);};

        //right axis 1D
        rightAxisActions = new Action<float>[availableAxis.Length];
        rightAxisActions[0] += (float x) => {rightTriggerAxis?.Invoke(x);};
        rightAxisActions[1] += (float x) => {rightGripAxis?.Invoke(x);};
        rightAxisActions[2] += (float x) => {rightBatteryLevelAxis?.Invoke(x);};
        
        //left axis 2D
        leftAxis2DActions = new Action<Vector2>[availableAxis2D.Length];
        leftAxis2DActions[0] += (Vector2 vec) => {leftPrimaryAxis2D?.Invoke(vec);};
        leftAxis2DActions[1] += (Vector2 vec) => {leftSecondaryAxis2D?.Invoke(vec);};

        //right axis 2D
        rightAxis2DActions = new Action<Vector2>[availableAxis2D.Length];
        rightAxis2DActions[0] += (Vector2 vec) => {rightPrimaryAxis2D?.Invoke(vec);};
        rightAxis2DActions[1] += (Vector2 vec) => {rightSecondaryAxis2D?.Invoke(vec);};

    }

    #endregion
}
