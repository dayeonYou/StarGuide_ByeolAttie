using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class MobileNotification : MonoBehaviour
{
    public void SendNotification()
    {
        int toggle1Value = PlayerPrefs.GetInt("Toggle1N", 0);

        if(toggle1Value==1){
            //Create the android notification channel to send message through
            var channel = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Notification Channel",
                Importance = Importance.Default,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            //Create the Notification that is going to be sent
            var notification = new AndroidNotification();
            notification.Title = "Byul";
            notification.Text = "We miss you!";
            notification.FireTime = System.DateTime.Now.AddSeconds(1);

            //Send the Notification
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
        
    }
}
