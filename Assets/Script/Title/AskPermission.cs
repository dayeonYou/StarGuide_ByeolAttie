using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IOS
using System.Runtime.InteropServices;
#endif

#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern bool hasIOSCameraPermission();
    [DllImport("__Internal")]
    private static extern void requestIOSCameraPermission();
#endif

public class AskPermission : MonoBehaviour
{
    void Start()
    {
        if (!HasCameraPermission())
        {
            RequestCameraPermission();
        }
    }

    bool HasCameraPermission()
    {
#if UNITY_ANDROID
        return Permission.HasUserAuthorizedPermission(Permission.Camera);
#elif UNITY_IOS
        return hasIOSCameraPermission();
#endif
    }

    void RequestCameraPermission()
    {
#if UNITY_ANDROID
        Permission.RequestUserPermission(Permission.Camera);
#elif UNITY_IOS
        requestIOSCameraPermission();
#endif
    }
}
