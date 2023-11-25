using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase;
using System;
using Firebase.Database;
public class SetDB : MonoBehaviour
{
    private FirebaseApp fbApp;
    private DatabaseReference reference;
    public string gameName;
    string[] InfoToGetArrayFindStar = {"rate","stareTime","count_eyes","count_face"}; 
    string[] InfoToGetArrayMouse = {"count","rTime","count_eyes","count_face"}; 
    string[] InfoToGetArrayTarget = {"gTime","sTime","count_eyes","count_face"}; 
    string[] InfoToGetArrayDressup = {"rate","clickTime","count_eyes","count_face"}; 
    string[] InfoToGetArrayTreasure = {"rate","playTime","count_eyes","count_face"};
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void setINFO()
    {
        string id = PlayerPrefs.GetString("UserID");
        switch(num){
            case 1:
                for(int j=4;j>-1;j--){
                    for(int k=0;k<4;k++){
                        DateTime today = DateTime.Today.AddDays(-j);
                        string dateString = today.ToString("yyyy-MM-dd");
                        string pathInfo = "User+"+id+"/Cancer/"+gameName+"/"+dateString+"/"+InfoToGetArrayFindStar[k];
                        if(InfoToGetArrayFindStar[k].Equals("rate")){
                            double value = (double)0.25* (Math.Abs(j-4));
                            setInfoOne(pathInfo, value);
                        }
                        else{
                            double value = 10*(k+j);
                            setInfoOne(pathInfo, value);
                        }
                    }
                }
                break;
            case 2:
                for(int j=4;j>-1;j--){
                    for(int k=0;k<4;k++){
                        DateTime today = DateTime.Today.AddDays(-j);
                        string dateString = today.ToString("yyyy-MM-dd");
                        string pathInfo = "User+"+id+"/Cancer/"+gameName+"/"+dateString+"/"+InfoToGetArrayMouse[k];
                        double value = (double)11*(j+k);
                        setInfoOne(pathInfo, value);
                    }
                }
                break;
            case 3:
                for(int j=4;j>-1;j--){
                    for(int k=0;k<4;k++){
                        DateTime today = DateTime.Today.AddDays(-j);
                        string dateString = today.ToString("yyyy-MM-dd");
                        string pathInfo = "User+"+id+"/Cancer/"+gameName+"/"+dateString+"/"+InfoToGetArrayTarget[k];
                        double value = 12*(j+k);
                        setInfoOne(pathInfo, value);
                    }
                }
                break;
            case 4:
                for(int j=4;j>-1;j--){
                    for(int k=0;k<4;k++){
                        DateTime today = DateTime.Today.AddDays(-j);
                        string dateString = today.ToString("yyyy-MM-dd");
                        string pathInfo = "User+"+id+"/Cancer/"+gameName+"/"+dateString+"/"+InfoToGetArrayDressup[k];
                        if(InfoToGetArrayDressup[k]=="rate"){
                            double value = (double)0.25* (Math.Abs(j-4));
                            setInfoOne(pathInfo, value);
                        }
                        else{
                            double value = 13*(j+k);
                            setInfoOne(pathInfo, value);
                        }
                    }
                }
                break;
            case 5:
                for(int j=4;j>-1;j--){
                    for(int k=0;k<4;k++){
                        DateTime today = DateTime.Today.AddDays(-j);
                        string dateString = today.ToString("yyyy-MM-dd");
                        string pathInfo = "User+"+id+"/Cancer/"+gameName+"/"+dateString+"/"+InfoToGetArrayTreasure[k];
                        if(InfoToGetArrayTreasure[k]=="rate"){
                            double value = (double)0.25* (Math.Abs(j-4));
                            setInfoOne(pathInfo, value);
                        }
                        else{
                            double value = 14*(k+j);
                            setInfoOne(pathInfo, value);
                        }
                    }
                }
                break;
            default:
                break;
        }
            

    }
    void setInfoOne(string path, double value){
        if(reference!=null){   
            Debug.Log("reference is not null2.");
        }
        else{
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        // 데이터베이스에 데이터 추가
        reference.Child(path).SetValueAsync(value).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("추가 실패: " + task.Exception);
            }
        });
    }
}
