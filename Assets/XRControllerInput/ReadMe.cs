/*
 * DESCRIPTION:
 * Unity's new XR Input System can be a little jarring, but with this helper script you'll 
 * have no problem receiving input events and hooking it up to your scripts. Has all the available
 * inputs, triggers, grip button, analog stick. I use this in every project without fail!
 *
 * If you have any questions or problems, reach out on Discord :D - https://discord.gg/YHKCQP6REp
 *
 * Packages also required for this one to work:
 * - Oculus Integration (available in unity asset store) https://assetstore.unity.com/publishers/25353
 *
 * INSTRUCTIONS:
 * This package is fairly simple, just make sure the prefab that comes with it is in the scene somewhere and then you
 * can start hooking up input events. It will not work if the prefab is not in the scene
 * Its got every input you could want, grip, trigger, analog stick, etc
 * You can take a look at all of them at the top of the file XRControllerInput.cs
 * Will work cross platform, except for a few key Oculus specific events -> index touch and thumb touch
 * Heres example usage: (In say a script that makes you jump when you press the right grip button)
 
 void OnEnable()
 {
     //assign a function to the press event
     XRControllerInput.rightGripButtonPressed += DoThing; <- make sure not to add the () after the function
 }
 
 void OnDisable()
 {
     //unsubscribe to clean up
     XRControllerInput.rightGripButtonPressed -= DoThing;
 }

 void DoThing()
 {
     //some functionality you want to happen when right grip button pressed
 }
 
 */
