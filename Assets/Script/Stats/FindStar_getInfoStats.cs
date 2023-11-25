using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class FindStar_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayFindStar = {"rate","stareTime","count_eyes","count_face"}; 

    string[] FindStarRate = new string[5]; 
    string[] FindStarStareTime = new string[5]; 
    string[] FindStarCount_Eyes = new string[5]; 
    string[] FindStarCount_Face = new string[5]; 
    
    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            FindStar_LoadStatsInfo();
        });
        
    }
    void update(){
        if(databaseReference!=null){   
            // Debug.Log("reference is not null2.");
        }
        else{
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        
        for(int l=0;l<5;l++){
            if(FindStarRate[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("rate",dateString, j, FindStarRate);
                }
                Debug.Log("findStar,rate null");
            }
        }
        for(int l=0;l<5;l++){
            if(FindStarStareTime[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("stareTime",dateString, j, FindStarStareTime);
                }
                Debug.Log("findStar,rate null");

            }
        }
        for(int l=0;l<5;l++){
            if(FindStarCount_Eyes[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("count_eyes",dateString, j, FindStarCount_Eyes);
                }
                Debug.Log("findStar,rate null");

            }
        }
        for(int l=0;l<5;l++){
            if(FindStarStareTime[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("count_face",dateString, j, FindStarCount_Face);
                }
                Debug.Log("findStar,rate null");

            }
        }
    }

    void FindStar_LoadStatsInfo(){
        //요소+날짜별
        for(int j=4;j>-1;j--){
            DateTime today = DateTime.Today.AddDays(-j); 
            string dateString = today.ToString("yyyy-MM-dd");
            GetStatsInfo("rate",dateString, j, FindStarRate);
            GetStatsInfo("stareTime",dateString, j, FindStarStareTime);
            GetStatsInfo("count_eyes",dateString, j, FindStarCount_Eyes);
            GetStatsInfo("count_face",dateString, j, FindStarCount_Face);
        }
    
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
        databaseReference.Child("User+"+CurrentUserID).Child(ConstellationName).Child("FindStar").Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] =  InfoValue;
                    Debug.Log("FindStar+InfoToGet"+InfoToGet+"date"+date+"InfoValue"+InfoValue);
                    Debug.Log("FindStar"+InfoToGet+saveInfoArray[j]+"/j"+j);
                }
                
            }
        });
    }
    public void printInfo(){
        for (int i = 0; i < FindStarRate.Length; i++)
        {
            if(FindStarRate[i]==null) FindStarRate[i] = "0";
        }
        for (int i = 0; i < FindStarRate.Length; i++)
        {
            if(FindStarStareTime[i]==null) FindStarStareTime[i] = "0";
        }
        for (int i = 0; i < FindStarRate.Length; i++)
        {
            if(FindStarCount_Eyes[i]==null) FindStarCount_Eyes[i] = "0";
        }
        for (int i = 0; i < FindStarRate.Length; i++)
        {
            if(FindStarCount_Face[i]==null) FindStarCount_Face[i] = "0";
        }
        for(int l=0;l<5;l++){
            Debug.Log("FindstarRate:"+FindStarRate[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("FindStarStareTime:"+FindStarStareTime[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("FindStarCount_Eyes:"+FindStarCount_Eyes[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("FindStarCount_Face:"+FindStarCount_Face[l]+"/l:"+l);
        }
    }
}

