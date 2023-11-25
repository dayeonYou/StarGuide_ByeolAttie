using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class Target_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayTarget = {"gTime","sTime","count_Eyes","count_Face"}; 

    string[] TargetGTime = new string[5]; 
    string[] TargetSTime = new string[5]; 
    string[] TargetCount_Eyes = new string[5];
    string[] TargetCount_Face = new string[5];
    
    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            TargetStar_LoadStatsInfo();
        });
    }

    void TargetStar_LoadStatsInfo(){
        //요소+날짜별
        for(int j=4;j>-1;j--){
            DateTime today = DateTime.Today.AddDays(-j); 
            string dateString = today.ToString("yyyy-MM-dd");
            GetStatsInfo("gTime",dateString, j, TargetGTime);
            GetStatsInfo("sTime",dateString, j, TargetSTime);
            GetStatsInfo("count_eyes",dateString, j, TargetCount_Eyes);
            GetStatsInfo("count_face",dateString, j, TargetCount_Face);
        }
    
    }
    void update(){
        if(databaseReference!=null){   
            // Debug.Log("reference is not null2.");
        }
        else{
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        // for(int l=0;l<5;l++){
        //     if(TargetGTime[l]==null){
        //         for(int j=4;j>-1;j--){
        //             DateTime today = DateTime.Today.AddDays(-j); 
        //             string dateString = today.ToString("yyyy-MM-dd");
        //             GetStatsInfo("gTime",dateString, j, TargetGTime);
        //         }
        //         Debug.Log("TargetGTime,rate null");
        //     }
        // }
        // for(int l=0;l<5;l++){
        //     if(TargetSTime[l]==null){
        //         for(int j=4;j>-1;j--){
        //             DateTime today = DateTime.Today.AddDays(-j); 
        //             string dateString = today.ToString("yyyy-MM-dd");
        //             GetStatsInfo("sTime",dateString, j, TargetSTime);
        //         }
        //         Debug.Log("TargetSTime,rate null");

        //     }
        // }
        // for(int l=0;l<5;l++){
        //     if(TargetCount_Eyes[l]==null){
        //         for(int j=4;j>-1;j--){
        //             DateTime today = DateTime.Today.AddDays(-j); 
        //             string dateString = today.ToString("yyyy-MM-dd");
        //             GetStatsInfo("count_eyes",dateString, j, TargetCount_Eyes);
        //         }
        //         Debug.Log("TargetCount_Eyes,rate null");

        //     }
        // }
        // for(int l=0;l<5;l++){
        //     if(TargetCount_Face[l]==null){
        //         for(int j=4;j>-1;j--){
        //             DateTime today = DateTime.Today.AddDays(-j); 
        //             string dateString = today.ToString("yyyy-MM-dd");
        //             GetStatsInfo("count_face",dateString, j, TargetCount_Face);
        //         }
        //         Debug.Log("TargetCount_Face,rate null");

        //     }
        // }
    }
    void GetStatsInfo(string InfoToGet,string date, int j, string[] saveInfoArray){

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        if(databaseReference!=null){   
            // Debug.Log("reference is not null2.");
        }
        else{
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        databaseReference.Child("User+"+CurrentUserID).Child(ConstellationName).Child("Target").Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] =  InfoValue;
                    Debug.Log("Target+date"+date+"InfoToGet"+InfoToGet+"InfoValue"+InfoValue);
                    Debug.Log("Target+"+InfoToGet+saveInfoArray[j]+"/j"+j);
                }
                
            }
        });
    }
    public void printInfo(){
        for(int l=0;l<5;l++){
            Debug.Log("Target TargetGTime:"+TargetGTime[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("Target TargetSTime:"+TargetSTime[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("Target TargetCount_Eyes:"+TargetCount_Eyes[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("Target TargetCount_Face:"+TargetCount_Face[l]+"/l:"+l);
        }
    }
}

