using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine.UI;
using TMPro;

public class FindStar_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayFindStar = {"rate","stareTime","count_eyes","count_face"}; 

    string[] FindStarRate = new string[5]; 
    string[] FindStarStareTime = new string[5]; 
    string[] FindStarCount_Eyes = new string[5]; 
    string[] FindStarCount_Face = new string[5];

    public RectTransform graphContainer; // 그래프를 담을 컨테이너 (RectTransform)
    public RectTransform graphContainer2; // 그래프를 담을 컨테이너 (RectTransform)
    public GameObject barPrefab; // 막대 그래프의 프리팹 (GameObject)
    public GameObject barPrefab2; // 막대 그래프의 프리팹 (GameObject)
    public float barWidth = 0f; // 막대의 너비
    public float spacing = 300f; // 막대 사이의 간격

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
            // UnityEngine.Debug.Log("reference is null2.");
        }
        
        for(int l=0;l<5;l++){
            if(FindStarRate[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("rate",dateString, j, FindStarRate);
                }
                UnityEngine.Debug.Log("findStar,rate null");
            }
        }
        for(int l=0;l<5;l++){
            if(FindStarStareTime[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("stareTime",dateString, j, FindStarStareTime);
                }
                UnityEngine.Debug.Log("findStar,rate null");

            }
        }
        for(int l=0;l<5;l++){
            if(FindStarCount_Eyes[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("count_eyes",dateString, j, FindStarCount_Eyes);
                }
                UnityEngine.Debug.Log("findStar,rate null");

            }
        }
        for(int l=0;l<5;l++){
            if(FindStarCount_Face[l]==null){
                for(int j=4;j>-1;j--){
                    DateTime today = DateTime.Today.AddDays(-j); 
                    string dateString = today.ToString("yyyy-MM-dd");
                    GetStatsInfo("count_face",dateString, j, FindStarCount_Face);
                }
                UnityEngine.Debug.Log("findStar,rate null");

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
            // UnityEngine.Debug.Log("reference is null2.");
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
                    UnityEngine.Debug.Log("FindStar+InfoToGet"+InfoToGet+"date"+date+"InfoValue"+InfoValue);
                    UnityEngine.Debug.Log("FindStar"+InfoToGet+saveInfoArray[j]+"/j"+j);
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
            UnityEngine.Debug.Log("FindstarRate:"+FindStarRate[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            UnityEngine.Debug.Log("FindStarStareTime:"+FindStarStareTime[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            UnityEngine.Debug.Log("FindStarCount_Eyes:"+FindStarCount_Eyes[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            UnityEngine.Debug.Log("FindStarCount_Face:"+FindStarCount_Face[l]+"/l:"+l);
        }

        float[] FindStarStareTimeF = Array.ConvertAll(FindStarStareTime, float.Parse);
        CreateBars(FindStarStareTimeF, new Vector2(0f, 0f));
        float[] FindStarRateFloat = Array.ConvertAll(FindStarRate, float.Parse);
        CreateBars2(FindStarRateFloat, new Vector2(0f, 0f));
    }


    public void printEye()
    {
        float[] FindStarCount_EyesF = Array.ConvertAll(FindStarCount_Eyes, float.Parse);
        CreateBars(FindStarCount_EyesF, new Vector2(0f, 0f));
    }

    public void printFace()
    {
        float[] FindStarCount_FaceF = Array.ConvertAll(FindStarCount_Face, float.Parse);
        CreateBars(FindStarCount_FaceF, new Vector2(0f, 0f));
    }

    void CreateBars(float[] values, Vector2 position)
    {
        float graphHeight = graphContainer.rect.height; // 그래프 컨테이너의 높이를 가져옴
        float totalBarWidth = (values.Length * barWidth) + ((values.Length - 1) * spacing); // 모든 막대의 너비와 간격의 총합 계산
        float startX = position.x - (totalBarWidth / 2f) + (barWidth / 2f); // 첫 번째 막대의 x축 위치 계산

        for (int i = 4; i > -1; i--)
        {
            int reverseIndex = values.Length - 1 - i; // 반대로 순회하기 위해 인덱스를 변환합니다.
            GameObject bar = Instantiate(barPrefab, graphContainer); // 막대 그래프 프리팹을 인스턴스화하여 생성
            RectTransform barRect = bar.GetComponent<RectTransform>(); // 막대의 RectTransform 컴포넌트 가져옴

            // 막대의 너비와 위치 설정
            float xPos = position.x + reverseIndex * (barWidth + spacing) - 10f;  // x축 위치 계산

            float barHeight;
            if (values[i] == 0) barHeight = 1f;
            else barHeight = (values[i] / 200f) * graphHeight;
            // 값을 기준으로 막대의 높이 계산 (0~200 범위를 그래프 컨테이너의 높이 범위로 변환)

            float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)
            barRect.sizeDelta = new Vector2(barWidth, barHeight); // 막대의 크기 설정
            barRect.anchoredPosition = new Vector2(xPos, (yPos + barHeight / 2f) - 20f);// 막대의 위치 설정 (막대의 중심을 그래프 컨테이너의 위쪽으로 향하도록 조정)

            UnityEngine.Debug.Log("1. FindStar barHeight:" + barHeight + "/l:" + i);
            UnityEngine.Debug.Log("1. FindStar xPos:" + xPos + "/l:" + i);
            UnityEngine.Debug.Log("1. FindStar yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
            barImage.color = new Color32(255, 192, 0, 255);
        }
    }

    // 비율 계산
    void CreateBars2(float[] values, Vector2 position)
    {
        float graphHeight = graphContainer2.rect.height; // 그래프 컨테이너의 높이를 가져옴
        float totalBarWidth = (values.Length * barWidth) + ((values.Length - 1) * spacing); // 모든 막대의 너비와 간격의 총합 계산
        float startX = position.x - (totalBarWidth / 2f) + (barWidth / 2f); // 첫 번째 막대의 x축 위치 계산

        for (int i = 4; i > -1; i--)
        {
            int reverseIndex = values.Length - 1 - i; // 반대로 순회하기 위해 인덱스를 변환합니다.
            GameObject bar = Instantiate(barPrefab2, graphContainer2); // 막대 그래프 프리팹을 인스턴스화하여 생성
            RectTransform barRect = bar.GetComponent<RectTransform>(); // 막대의 RectTransform 컴포넌트 가져옴

            // 막대의 너비와 위치 설정
            float xPos = position.x + reverseIndex * (barWidth + spacing) - 10f;  // x축 위치 계산

            float barHeight;
            if (values[i] == 0) barHeight = 1f;
            else barHeight = values[i] * graphHeight - 80f;
            // 값을 기준으로 막대의 높이 계산 (0~200 범위를 그래프 컨테이너의 높이 범위로 변환)float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)

            float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)
            barRect.sizeDelta = new Vector2(barWidth, barHeight); // 막대의 크기 설정
            barRect.anchoredPosition = new Vector2(xPos, (yPos + barHeight / 2f) - 20f); // 막대의 위치 설정 (막대의 중심을 그래프 컨테이너의 위쪽으로 향하도록 조정)

            UnityEngine.Debug.Log("2. FindStar barHeight:" + barHeight + "/l:" + i);
            UnityEngine.Debug.Log("2. FindStar xPos:" + xPos + "/l:" + i);
            UnityEngine.Debug.Log("2. FindStar yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
            barImage.color = new Color32(255, 192, 0, 255);
        }
    }
}

