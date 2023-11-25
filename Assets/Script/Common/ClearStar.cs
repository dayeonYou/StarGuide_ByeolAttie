using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;
using System;
using Firebase;
public class ClearStar : MonoBehaviour
{
    public string ConstellationName;
    public int nextStarCount;
    DatabaseReference databaseReference;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void getClearStars()
    {
        if(databaseReference!=null){   
            Debug.Log("reference is not null2.");
        }
        else{
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");

        databaseReference.Child("User+"+CurrentUserID).Child("Constellation").Child(ConstellationName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    int count = int.Parse(snapshot.Value.ToString());
                    if(count == (nextStarCount -1) ){
                        setClearStars();
                    }
                }
            }
        });
    }
    public void setClearStars(){
        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        string path_ToNextStar = "User+"+CurrentUserID+"/Constellation/"+ConstellationName;
        if(databaseReference!=null){   
            Debug.Log("reference is not null2.");
        }
        else{
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        databaseReference.Child(path_ToNextStar).SetValueAsync(nextStarCount).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError(" 추가 실패: " + task.Exception);
            }
        });
    }
}
