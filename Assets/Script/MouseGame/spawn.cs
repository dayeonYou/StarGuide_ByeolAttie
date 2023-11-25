using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class spawn : MonoBehaviour
{
    public Mouse_EyeTrackingScript Mouse_EyeTrackingScript;
    public TimeLimited TimeLimited;

    public GameObject playerPrefab; // �÷��̾� �������� �Ҵ��� ����
    public Transform spawnPoint; // ���� ��ġ�� �Ҵ��� ����

    public GameObject currentPlayer; // ���� ������ �÷��̾ ������ ����

    private float scaleFactor;
    public int maxCount = 3;
    public static string OverString = "";

    public pause pause;
    DatabaseReference databaseReference;
    public string ConstellationName;

    public void Start()
    {
        OverString = "";
        SpawnPlayer();
        Mouse_EyeTrackingScript=FindObjectOfType<Mouse_EyeTrackingScript>();
        TimeLimited=FindObjectOfType<TimeLimited>();

        if (!pause) Debug.LogError("Pause script is not assigned");

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            //currentPlayer.transform.localScale = Vector3.one * initialScale;
        }
        else
        {
            Debug.LogError("GameManager is missing playerPrefab or spawnPoint references.");
        }
    }

    public void Update()
    {
        if (currentPlayer == null)
        {
            RespawnPlayer();
        }
    } 

    public void RespawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            if (CountText.count < maxCount) // 3���� ����
            {
                pause.StartPanel.SetActive(true); // �ε����� �� �ٽ� ���� ����

                currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
                IncreasePlayerSize();
                Debug.Log("ũ�Ⱑ Ŀ��");
                Debug.Log("�ε��� Ƚ�� : " + CountText.count);
            }
            else
            {
                Time.timeScale = 0f;
                OverString = "GameOver";
                UnityEngine.Debug.Log("Game Over!" + OverString);
                Debug.Log("�ε��� Ƚ�� : " + CountText.count + "-���ӿ��� ����");

                int count_faceValue = Mouse_EyeTrackingScript.count_face;
                int count_eyesValue = Mouse_EyeTrackingScript.count_eyes;
                float rTime         = TimeLimited.rTime;
                int count           = CountText.count;
                
                DateTime today = DateTime.Today;
                string dateString = today.ToString("yyyy-MM-dd");
                Debug.Log(dateString);

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
            };
        }
        else
        {
            Debug.LogError("GameManager is missing playerPrefab or spawnPoint references.");
        }
    }

    private void IncreasePlayerSize()
    {
        scaleFactor += (CountText.count * 0.025f);
        Vector3 newSize = currentPlayer.transform.localScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
        currentPlayer.transform.localScale = newSize;
    }

}
