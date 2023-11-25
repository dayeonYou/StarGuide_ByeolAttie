using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class Dressup_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayDressup = {"rate","clickTime","count_eyes","count_face"}; 

    string[] DressupRate = new string[5]; 
    string[] DressupClickTime = new string[5]; 
    string[] DressupCount_Eyes = new string[5]; 
    string[] DressupCount_Face = new string[5]; 
    
    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            Dressup_LoadStatsInfo();
        });
    }

    void Dressup_LoadStatsInfo(){
        //요소+날짜별
        for(int j=4;j>-1;j--){
            DateTime today = DateTime.Today.AddDays(-j); 
            string dateString = today.ToString("yyyy-MM-dd");
            GetStatsInfo("rate",dateString, j, DressupRate);
            GetStatsInfo("clickTime",dateString, j, DressupClickTime);
            GetStatsInfo("count_eyes",dateString, j, DressupCount_Eyes);
            GetStatsInfo("count_face",dateString, j, DressupCount_Face);
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
        databaseReference.Child("User+"+CurrentUserID).Child(ConstellationName).Child("Dressup").Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] =  InfoValue;
                    Debug.Log("Dressup+date"+date+"InfoToGet"+InfoToGet+"InfoValue"+InfoValue);
                    Debug.Log("Dressup+saveInfoArray[j]"+saveInfoArray[j]+"/j"+j);
                }
                
            }
        });
    }
    public void printInfo(){
        for(int l=0;l<5;l++){
            Debug.Log("DressupRate:"+DressupRate[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("DressupClickTime:"+DressupClickTime[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("DressupCount_Eyes:"+DressupCount_Eyes[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("DressupCount_Face:"+DressupCount_Face[l]+"/l:"+l);
        }
    }
}

