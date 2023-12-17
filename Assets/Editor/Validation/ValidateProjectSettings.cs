using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Scripting;

/*
 * Make sure some project settings are set that will cause breaks others
 */

public class ValidateProjectSettings
{
    [MenuItem("Validation/Validate Project Settings For Release Build")]
    public static void ValidateSettings()
    {
        Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None); //for legibility

        if (QualitySettings.vSyncCount > 0)
            Debug.Log("V sync turned on, turning off");
        QualitySettings.vSyncCount = 0;

        if (!GarbageCollector.isIncremental)
            Debug.Log("Garbage collection is using non-incremental, please change");

        if (PlayerSettings.colorSpace == ColorSpace.Gamma)
            Debug.Log("Color space is set to Gamma, Please change to Linear");

        PlayerSettings.companyName = "Sol 5 Studios";

        if (QualitySettings.antiAliasing != 4)
        {
            Debug.Log("AntiAliasing not set to 4x MSAA, turning on");
            QualitySettings.antiAliasing = 4;
        }
        
        // TestManager tm = new TestManager();
        // if (TestManager.useLocal == true)
            // Debug.Log("TestManager.uselocal is set to true, set it to false");
        // Object.DestroyImmediate(tm);
        //edit this
        //Platform specific stuff
#if UNITY_ANDROID
        Debug.Log("Make sure to update the version number");
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
        PlayerSettings.Android.bundleVersionCode++; //increase the build number
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;

        if (PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android) == ScriptingImplementation.Mono2x)
            Debug.Log("Scripting backend set to Mono, Please change to IL2CPP");
#else
        
        //I can't find the equivalent build number for PC, I'll have to remember to increase by hand
        // Debug.Log(PlayerSettings.bundleVersion);
        // Debug.Log(Application.version);
        Debug.Log("Make sure to update the build number!!!");

        if (PlayerSettings.GetScriptingBackend(BuildTargetGroup.Standalone) == ScriptingImplementation.Mono2x)
            Debug.Log("Scripting backend set to Mono, Please change to IL2CPP");
#endif
        Debug.Log("Push the project to Unity cloud storage");
        Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.ScriptOnly); //revert
    }
}

//so make debug builds and test their optimizations
//Make a local emulator one
//Upload the project to Oculus and test if its different in store
//Copy the project

