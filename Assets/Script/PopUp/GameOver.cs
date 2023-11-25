using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class GameOver : MonoBehaviour
{
    public int maxCount = 3;
    public GameObject gameOverPanel;
    bool isGameOver = false;
    public Target_GazeInteraction Target_GazeInteraction;    
    public Target_EyeTrackingScript target_EyeTrackingScript;
    DatabaseReference databaseReference;
    public string ConstellationName;
    void Start()
    {
        target_EyeTrackingScript=FindObjectOfType<Target_EyeTrackingScript>();
        Target_GazeInteraction=FindObjectOfType<Target_GazeInteraction>();

        gameOverPanel = GameObject.Find("GameOver");
        if(gameOverPanel != null)
        {
            if (!isGameOver) gameOverPanel.SetActive(false);
        }
        else
            Debug.Log("No gameOverPanel called panel found");
        UnityEngine.Debug.Log("���ӿ���! ��Ȱ��ȭ");

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLimited.rTime == 0 || Timesys.gTime == 120 || spawn2.OverString == "GameOver" || mousepoint.OverString == "GameOver")
        {
            UnityEngine.Debug.Log("Update ���ӿ���! Ȯ��" + spawn2.OverString);
            isGameOver = true;
            if(isGameOver) Show();
            UnityEngine.Debug.Log("���ӿ���! Ȱ��ȭ");

            int count_faceValue = target_EyeTrackingScript.count_face;
            int count_eyesValue = target_EyeTrackingScript.count_eyes;
            float gTime         = Timesys.gTime;
            float sTime         = Target_GazeInteraction.Timer;
            DateTime today = DateTime.Today;
            string dateString = today.ToString("yyyy-MM-dd");
            Debug.Log(dateString);
            string CurrentUserID = "";
            CurrentUserID = PlayerPrefs.GetString("UserID");
            string path_count_faceValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/count_face";
            string path_count_eyesValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/count_eyes";
            string path_gTime           = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/gTime";
            string path_sTime           = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/sTime";

            if(databaseReference!=null){   
                Debug.Log("reference is not null2.");
            }
            else{
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("reference is null2.");
            }

            databaseReference.Child(path_count_faceValue).SetValueAsync(count_faceValue).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    UnityEngine.Debug.Log("추가 성공!");
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.LogError(" 추가 실패: " + task.Exception);
                }
            });
            databaseReference.Child(path_count_eyesValue).SetValueAsync(count_eyesValue).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    UnityEngine.Debug.Log(" 추가 성공!");
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.LogError(" 추가 실패: " + task.Exception);
                }
            });
            databaseReference.Child(path_gTime).SetValueAsync(gTime).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    UnityEngine.Debug.Log(" 추가 성공!");
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.LogError(" 추가 실패: " + task.Exception);
                }
            });
            databaseReference.Child(path_sTime).SetValueAsync(sTime).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    UnityEngine.Debug.Log(" 추가 성공!");
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.LogError(" 추가 실패: " + task.Exception);
                }
            });
        } 
    }
    void Show()
    {
        gameOverPanel.SetActive(true);
    }
}
