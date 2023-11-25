using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class treasure_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayTreasure = {"rate","playTime","count_eyes","count_face"}; 

    string[] treasureRate = new string[5]; 
    string[] treasurePlayTime = new string[5];  
    string[] treasureCount_Eyes = new string[5]; 
    string[] treasureCount_Face = new string[5];  
    
    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            treasure_LoadStatsInfo();
        });
    }

    void treasure_LoadStatsInfo(){
        //요소+날짜별
        for(int j=4;j>-1;j--){
            DateTime today = DateTime.Today.AddDays(-j); 
            string dateString = today.ToString("yyyy-MM-dd");
            GetStatsInfo("rate",dateString, j, treasureRate);
            GetStatsInfo("playTime",dateString, j, treasurePlayTime);
            GetStatsInfo("count_eyes",dateString, j, treasureCount_Eyes);
            GetStatsInfo("count_face",dateString, j, treasureCount_Face);
        }
    
    }

    void GetStatsInfo(string InfoToGet,string date, int j, string[] saveInfoArray){

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        if(databaseReference!=null){   
            Debug.Log("reference is not null2.");
        }
        else{
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        databaseReference.Child("User+"+CurrentUserID).Child(ConstellationName).Child("treasure").Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] =  InfoValue;
                    Debug.Log("treasure+date"+date+"InfoToGet"+InfoToGet+"InfoValue"+InfoValue);
                    Debug.Log("treasure+saveInfoArray[j]"+saveInfoArray[j]+"/j"+j);
                }
                
            }
        });
    }
    public void printInfo(){
        for(int l=0;l<5;l++){
            Debug.Log("treasure Rate:"+treasureRate[l]+"l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("treasure PlayTime:"+treasurePlayTime[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("treasure Count_Eyes:"+treasureCount_Eyes[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("treasure Count_Face:"+treasureCount_Face[l]+"/l:"+l);
        }
    }
}

