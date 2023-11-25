using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class CallVibrate : MonoBehaviour
{
    public void Send()
    {
        int toggle1Value = PlayerPrefs.GetInt("Toggle1", 0);

        if(toggle1Value==1){
            Vibration.Vibrate(800);
        }
        
    }
}
