using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseReward : MonoBehaviour
{
    DatabaseReference databaseReference;
    // public string ClearConstellation;
    private void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // LoadAndDeactivateReward();

        });
    }

    // private void LoadAndDeactivateReward()
    // {
    //     string CurrentUserID = "";
    //     CurrentUserID = PlayerPrefs.GetString("UserID");
    //     // CurrentUserID = bQMkmHlOM6VYYVYv5VCXnBXQSh13
    //     databaseReference.Child("User+"+CurrentUserID).Child("Constellation").Child("Clear").GetValueAsync().ContinueWithOnMainThread(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             DataSnapshot snapshot = task.Result;
    //             if (snapshot.Exists)
    //             {
    //                 ClearConstellation = snapshot.Value.ToString();
    //                 Debug.Log(ClearConstellation+"clear!!");
    //             }
    //             else{
    //                 Debug.Log("notFound!");
    //             }
                
    //         }
    //         else{
    //             Debug.Log("notYet!");
    //         }
    //     });
    // }
}
