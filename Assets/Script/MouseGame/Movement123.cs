using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class Movement123 : MonoBehaviour
{
    //private float moveSpeed = 5f;
    private Rigidbody2D rigid2D;
    public static string ClearString = "";
    public Transform spawnPoint;

    public static string StartStr = "";
    public bool canMove = false;

    //public GameClear gameClear;
    DatabaseReference databaseReference;
    public string ConstellationName;

    private void Start()
    {
        ClearString = "";
        StartStr = "";
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
        GameObject eyeTrackingScriptObject = GameObject.Find("EyeTracking");
        if (eyeTrackingScriptObject != null)
        {
            Mouse_EyeTrackingScript eyeTrackingScript = eyeTrackingScriptObject.GetComponent<Mouse_EyeTrackingScript>();
            if (eyeTrackingScript != null)
            {
                ConstellationName = eyeTrackingScript.ConstellationName;
            }
        }
    }

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            CountText.count += 1;
            Destroy(gameObject);
            UnityEngine.Debug.Log("벽에 닿았음");
            if(CountText.count == 1)
            {
                Destroy(GameObject.Find("heart1"));
            }
            else if(CountText.count == 2) {
                Destroy(GameObject.Find("heart1 (1)"));
            }
            else if(CountText.count == 3)
            {
                Destroy(GameObject.Find("heart1 (2)"));
            }

        }

        if (collision.CompareTag("Finish"))
        {
            ClearString = "Clear";
            Time.timeScale = 0f;    // 클리어하면 화면 멈춤
            UnityEngine.Debug.Log("클리어!" + ClearString);
            //gameClear.Show();

            int count_faceValue = 0;
            int count_eyesValue = 0;
            GameObject eyeTrackingScriptObject = GameObject.Find("EyeTracking");
            GameObject ConstellationNameScriptObject = GameObject.Find("ConstellationName");

            if (eyeTrackingScriptObject != null)
            {
                Mouse_EyeTrackingScript eyeTrackingScript = eyeTrackingScriptObject.GetComponent<Mouse_EyeTrackingScript>();
                if (eyeTrackingScript != null)
                {
                    count_eyesValue = eyeTrackingScript.count_eyes;
                    count_faceValue = eyeTrackingScript.count_face;
                }
            }
            if (ConstellationNameScriptObject != null)
            {
                ConstellationNameScript constellationNameScript = ConstellationNameScriptObject.GetComponent<ConstellationNameScript>();
                if (constellationNameScript != null)
                {
                    ConstellationName = constellationNameScript.ConstellationName;
                }
            }
            DateTime today = DateTime.Today;
            string dateString = today.ToString("yyyy-MM-dd");
            UnityEngine.Debug.Log(dateString);           
            
            float rTime         = TimeLimited.rTime;
            int count           = CountText.count;

            string CurrentUserID = "";
            CurrentUserID = PlayerPrefs.GetString("UserID");
            string path_count_faceValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Mouse/"+dateString+"/count_face";
            string path_count_eyesValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Mouse/"+dateString+"/count_eyes";
            string path_count           = "User+"+CurrentUserID+"/"+ConstellationName+"/Mouse/"+dateString+"/count";
            string path_rTime           = "User+"+CurrentUserID+"/"+ConstellationName+"/Mouse/"+dateString+"/rTime";
            if(databaseReference!=null){   
                UnityEngine.Debug.Log("reference is not null2.");
            }
            else{
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                UnityEngine.Debug.Log("reference is null2.");
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
            databaseReference.Child(path_count).SetValueAsync(count).ContinueWithOnMainThread(task =>
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
            databaseReference.Child(path_rTime).SetValueAsync(rTime).ContinueWithOnMainThread(task =>
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