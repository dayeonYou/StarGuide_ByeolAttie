using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] gameNameArray = {"FindStar","Mouse","Target","Dressup","treasure"}; 
    
    string[] InfoToGetArrayFindStar = {"rate","stareTime","count_eyes","count_face"}; 
    string[] InfoToGetArrayMouse = {"count","rTime","count_eyes","count_face"}; 
    string[] InfoToGetArrayTarget = {"gTime","sTime","count_Eyes","count_Face"}; 
    string[] InfoToGetArrayDressup = {"rate","clickTime","count_Eyes","count_Face"}; 
    string[] InfoToGetArrayTreasure = {"rate","playTime","count_Eyes","count_Face"}; 

    string[] FindStarRate = new string[5]; 
    string[] FindStarStareTime = new string[5]; 
    string[] FindStarCount_Eyes = new string[5]; 
    string[] FindStarCount_Face = new string[5]; 
    
    string[] MouseCount = new string[5]; 
    string[] MouseRTime = new string[5]; 
    string[] MouseCount_Eyes = new string[5]; 
    string[] MouseCount_Face = new string[5]; 

    string[] TargetGTime = new string[5]; 
    string[] TargetSTime = new string[5]; 
    string[] TargetCount_Eyes = new string[5];
    string[] TargetCount_Face = new string[5];

    string[] DressupRate = new string[5]; 
    string[] DressupClickTime = new string[5]; 
    string[] DressupCount_Eyes = new string[5]; 
    string[] DressupCount_Face = new string[5]; 

    string[] treasureRate = new string[5]; 
    string[] treasurePlayTime = new string[5];  
    string[] treasureCount_Eyes = new string[5]; 
    string[] treasureCount_Face = new string[5]; 

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            LoadStatsInfo();
            
        });
    }

    void LoadStatsInfo(){
        //게임이름+요소+날짜별
        for(int i=0;i<5;i++){ 
            for(int k=0;k<4;k++){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    if(i==0){
                        switch(k){
                            case 0:
                                GetStatsInfo(gameNameArray[i],InfoToGetArrayFindStar[k],dateString, j, FindStarRate);
                                // Debug.Log("date"+dateString);
                                // Debug.Log("gameNameArray[i]"+gameNameArray[i]);
                                // Debug.Log("InfoToGetArrayFindStar[k]"+InfoToGetArrayFindStar[k]);
                                // Debug.Log("j"+j);
                                
                                break;
                            case 1:
                                GetStatsInfo(gameNameArray[i],InfoToGetArrayFindStar[k],dateString, j, FindStarStareTime);
                                break;
                            case 2:
                                GetStatsInfo(gameNameArray[i],InfoToGetArrayFindStar[k],dateString, j, FindStarCount_Eyes);
                                break;
                            case 3:
                                GetStatsInfo(gameNameArray[i],InfoToGetArrayFindStar[k],dateString, j, FindStarCount_Face);
                                break;
                            
                            default:
                                break;
                        }
                    }
                    // if(i==1){
                    //     switch(k){
                    //         case 0:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayMouse[k],dateString, j, MouseCount);
                    //             break;
                    //         case 1:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayMouse[k],dateString, j, MouseRTime);
                    //             break;
                    //         case 2:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayMouse[k],dateString, j, MouseCount_Eyes);
                    //             break;
                    //         case 3:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayMouse[k],dateString, j, MouseCount_Face);
                    //             break;
                    //         default:
                    //             break;
                    //     }
                    // }
                    // if(i==2){
                    //     switch(k){
                    //         case 0:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTarget[k],dateString, j, TargetGTime);
                    //             break;
                    //         case 1:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTarget[k],dateString, j, TargetSTime);
                    //             break;
                    //         case 2:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTarget[k],dateString, j, TargetCount_Eyes);
                    //             break;
                    //         case 3:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTarget[k],dateString, j, TargetCount_Face);
                    //             break;
                    //         default:
                    //             break;
                    //     }
                    // }
                    // if(i==3){
                    //     switch(k){
                    //         case 0:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayDressup[k],dateString, j, DressupRate);
                    //             break;
                    //         case 1:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayDressup[k],dateString, j, DressupClickTime);
                    //             break;
                    //         case 2:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayDressup[k],dateString, j, DressupCount_Eyes);
                    //             break;
                    //         case 3:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayDressup[k],dateString, j, DressupCount_Face);
                    //             break;
                    //         default:
                    //             break;
                    //     }
                    // }
                    // if(i==4){
                    //     switch(k){
                    //         case 0:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTreasure[k],dateString, j, treasureRate);
                    //             break;
                    //         case 1:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTreasure[k],dateString, j, treasurePlayTime);
                    //             break;
                    //         case 2:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTreasure[k],dateString, j, treasureCount_Eyes);
                    //             break;
                    //         case 3:
                    //             GetStatsInfo(gameNameArray[i],InfoToGetArrayTreasure[k],dateString, j, treasureCount_Face);
                    //             break;
                    //         default:
                    //             break;
                    //     }
                    // }
                   
                    
                    
                    
                }
            }
        }
    }

    void GetStatsInfo(string gameName, string InfoToGet,string date, int j, string[] saveInfoArray){

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");

        databaseReference.Child("User+"+CurrentUserID).Child(ConstellationName).Child(gameName).Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] =  InfoValue;
                    Debug.Log("gameName"+gameName+"date"+date+"InfoToGet"+InfoToGet+"InfoValue"+InfoValue);
                    Debug.Log("saveInfoArray[j]"+saveInfoArray[j]+"/j"+j);

                    // if(snapshot.Value == null){
                    //     
                    //     Debug.Log(null+j);
                    // }
                }
                else{
                    // string InfoValue2 = "";
                    // saveInfoArray[(j)] = InfoValue2;
                    // Debug.Log("it is null"+saveInfoArray[(j)]);
                }
                
            }
        });
    }

    public void printInfo(){
        for(int l=0;l<5;l++){
            Debug.Log("findstarRate:"+FindStarRate[l]+"/l:"+l);
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
