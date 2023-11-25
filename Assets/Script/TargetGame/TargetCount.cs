using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class TargetCount : MonoBehaviour
{
    private TextMeshProUGUI text;
    public static int count = 0;
    public static string ClearString = "";

    public Target_EyeTrackingScript target_EyeTrackingScript;
    public Target_GazeInteraction Target_GazeInteraction;

    DatabaseReference databaseReference;
    public string ConstellationName;
    private void Start()
    {
        target_EyeTrackingScript=FindObjectOfType<Target_EyeTrackingScript>();
        Target_GazeInteraction=FindObjectOfType<Target_GazeInteraction>();

        ClearString = "";
        count = 0;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("CountText script requires a Text component.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            //text.text = "COUNT : " + count.ToString();
            text.text = count.ToString();
        }

        if (count == 5)
        {
            ClearString = "Clear";
            Time.timeScale = 0f;    // Ŭ�����ϸ� ȭ�� ����
            UnityEngine.Debug.Log("Ŭ����!" + ClearString);

            if(databaseReference!=null){   
                Debug.Log("reference is not null2.");
            }
            else{
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("reference is null2.");
            }
            DateTime today = DateTime.Today;
            string dateString = today.ToString("yyyy-MM-dd");
            Debug.Log(dateString);
            
            int count_faceValue = target_EyeTrackingScript.count_face;
            int count_eyesValue = target_EyeTrackingScript.count_eyes;
            float gTime         = Timesys.gTime;
            float sTime         = Target_GazeInteraction.Timer;

            string CurrentUserID = "";
            CurrentUserID = PlayerPrefs.GetString("UserID");
            string path_count_faceValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/count_face";
            string path_count_eyesValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/count_eyes";
            string path_gTime           = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/gTime";
            string path_sTime           = "User+"+CurrentUserID+"/"+ConstellationName+"/Target/"+dateString+"/sTime";

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
}
