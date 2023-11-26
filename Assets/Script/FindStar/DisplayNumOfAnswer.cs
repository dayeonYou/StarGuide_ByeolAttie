using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 시 추가
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
public class DisplayNumOfAnswer : MonoBehaviour
{
    public Find_Tracking Find_Tracking;
    public DisplayRound DisplayRound;
    public Find_DetectCollision Find_DetectCollision;
    public MakeAnswer MakeAnswer;

    public Button stBtn;
    public Sprite disabledImage;

    public TextMeshProUGUI numOfAnswerText; // TextMeshPro 사용 시 수정
    public int maxRound = 3;
    DisplayRound displayRoundScript;
    public int numOfAnswer = 0;
    public int clearCondition;
    public TextMeshProUGUI textMeshPro_clearMessage;
    public TextMeshProUGUI textMeshPro_overMessage;

    public TextMeshProUGUI textMeshPro_clearText;
    public TextMeshProUGUI textMeshPro_overText;

    public GameObject btnB;
    public GameObject btnR;
    public GameObject btnN;
    public GameObject CorrectAnswerImage;

    public GameObject squareWindowGame; 

    DatabaseReference databaseReference;
    public string ConstellationName;
    public int nextStarCount;

    void Start()
    {
        displayRoundScript = FindObjectOfType<DisplayRound>();
        //maxRound = displayRoundScript.maxClicks;

        numOfAnswerText = GameObject.Find("AnswerNumber").GetComponent<TextMeshProUGUI>(); // TextMeshPro 사용 시 수정
        if(numOfAnswerText==null){
            Debug.Log("nullText");
        }
        else{
            //numOfAnswerText.text = "찾은 별의 개수 : " + $"{numOfAnswer} / {clearCondition}"; // 초기값 표시
            numOfAnswerText.text = $"{numOfAnswer}/{clearCondition}"; // 초기값 표시
        }
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void OnAnswerCorrectly()
    {
        numOfAnswer++;

        // 클릭 횟수 텍스트 업데이트
        //numOfAnswerText.text = "찾은 별의 개수 : " + $"{numOfAnswer} / {clearCondition}";
        numOfAnswerText.text = $"{numOfAnswer}/{clearCondition}";
        CorrectAnswerImage.SetActive(true);
        StartCoroutine(SetObjectActive());

        // 작업 수행 코드_게임 클리어
        if (numOfAnswer == clearCondition){
            AddDataToDatabase2();
            squareWindowGame.SetActive(true);
            textMeshPro_clearMessage.gameObject.SetActive(true);
            textMeshPro_overMessage.gameObject.SetActive(false);

            textMeshPro_clearText.gameObject.SetActive(true);
            textMeshPro_overText.gameObject.SetActive(false);

            btnB.SetActive(true);
            btnN.SetActive(true);
            Find_Tracking.stopTracking();

            stBtn.interactable = false;
            Image image = stBtn.GetComponent<Image>();
            if (image != null && disabledImage != null)
            {
                image.sprite = disabledImage;
            }
            else{
                Debug.Log("disabled Image");
            }

            
        }

    }
    public void AddDataToDatabase2(){
        int count_faceValue = Find_Tracking.count_face;
        int count_eyesValue = Find_Tracking.count_eyes;
        int indexHeartV = Math.Abs((MakeAnswer.indexHeart) - 3);
        int count_click = DisplayRound.clickCount + indexHeartV;

        // int numOfAnswer = count_click - 3;
        double rate = (double)numOfAnswer / count_click;
        float stareTime = Find_DetectCollision.sec;

        // int count_faceValue = 1;
        // int count_eyesValue = 2;
        // int count_click = 3;
        // int rate = 4;
        
        Debug.Log("count1: "+count_faceValue);
        Debug.Log("count2: "+count_eyesValue);
        Debug.Log("count click: "+count_click);
        Debug.Log("numofanswer: "+numOfAnswer);
        Debug.Log("stareTime: "+stareTime);

        Debug.Log("rate: "+rate);
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

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        string path_count_faceValue = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/count_face";
        string path_count_eyesValue = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/count_eyes";
        string path_count_rate = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/rate";
        string path_count_stareTime = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/stareTime";


        databaseReference.Child(path_count_faceValue).SetValueAsync(count_faceValue).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError(" 추가 실패: " + task.Exception);
            }
        });
        databaseReference.Child(path_count_eyesValue).SetValueAsync(count_eyesValue).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log(" 추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError(" 추가 실패: " + task.Exception);
            }
        });
        databaseReference.Child(path_count_rate).SetValueAsync(rate).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log(" 추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError(" 추가 실패: " + task.Exception);
            }
        });
        
        databaseReference.Child(path_count_stareTime).SetValueAsync(stareTime).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log(" 추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError(" 추가 실패: " + task.Exception);
            }
        });
    }
    IEnumerator SetObjectActive(){
        yield return new WaitForSeconds(3f);

        CorrectAnswerImage.SetActive(false);
    }
}
