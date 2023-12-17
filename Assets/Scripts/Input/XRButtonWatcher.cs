using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

/*
 * A class to watch a certain XR button for input
 */

public class XRButtonWatcher : MonoBehaviour
{
    readonly Dictionary<string, InputFeatureUsage<bool>> availableButtons = new Dictionary<string, InputFeatureUsage<bool>>
    {
        {"triggerButton", CommonUsages.triggerButton },
        {"thumbrest", OculusUsages.thumbrest },
        {"primary2DAxisClick", CommonUsages.primary2DAxisClick },
        {"primary2DAxisTouch", CommonUsages.primary2DAxisTouch },
        {"menuButton", CommonUsages.menuButton },
        {"gripButton", CommonUsages.gripButton },
        {"secondaryButton", CommonUsages.secondaryButton },
        {"secondaryTouch", CommonUsages.secondaryTouch },
        {"primaryButton", CommonUsages.primaryButton },
        {"primaryTouch", CommonUsages.primaryTouch },
    };

    public enum ButtonOption
    {
        triggerButton,
        thumbrest,
        primary2DAxisClick,
        primary2DAxisTouch,
        menuButton,
        gripButton,
        secondaryButton,
        secondaryTouch,
        primaryButton,
        primaryTouch
    };

    public enum Hand {Left, Right}

    public Hand handToWatch = Hand.Left;
    public ButtonOption buttonToWatch;
    public UnityEvent buttonPressed;
    public UnityEvent buttonReleased;

    private InputFeatureUsage<bool> buttonToWatchFeature;
    private bool lastButtonState = false;
    private List<InputDevice> devicesWithButton;

    private void Awake()
    {
        if (buttonPressed == null)
        {
            buttonPressed = new UnityEvent();
            buttonReleased = new UnityEvent();
        }

        devicesWithButton = new List<InputDevice>();
    }

    void OnEnable()
    {
        buttonToWatchFeature = availableButtons[buttonToWatch.ToString()];
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithButton.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        if (handToWatch == Hand.Right && (device.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left) //filter left hands
            return;
        if (handToWatch == Hand.Left && (device.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right) //filter right hands
            return;

        bool discardedValue;
        if (device.TryGetFeatureValue(buttonToWatchFeature, out discardedValue))
        {
            devicesWithButton.Add(device); // Add any devices that has the button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithButton.Contains(device))
            devicesWithButton.Remove(device);
    }

    bool pressed = false; //pressing flag
    void Update()
    {
        // bool tempState = false;
        foreach (var device in devicesWithButton)
        {
            bool inputValue = false;
            // tempState = device.TryGetFeatureValue(buttonToWatchFeature, out inputValue) && inputValue;
            if (device.TryGetFeatureValue(buttonToWatchFeature, out inputValue) && inputValue)
            {
                //they've pressed the button
                if (!pressed)
                {
                    pressed = true;
                    buttonPressed?.Invoke();
                }
            }
            // they've released the button
            else if (pressed)
            {
                pressed = false;
                buttonReleased?.Invoke();
            }
        }
        // if (tempState != pressed) // Button state changed since last frame
        // {
        //     if (tempState == true)
        //         buttonPressed?.Invoke();
        //     else
        //         buttonReleased?.Invoke();
        //     // primaryButtonPress.Invoke(tempState);
        //     pressed = tempState;
        // }
    }
}