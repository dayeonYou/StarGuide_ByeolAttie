using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine.UI;

public class treasure_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayTreasure = {"rate","playTime","count_eyes","count_face"}; 

    string[] treasureRate = new string[5]; 
    string[] treasurePlayTime = new string[5];  
    string[] treasureCount_Eyes = new string[5]; 
    string[] treasureCount_Face = new string[5];

    public RectTransform graphContainer; // 그래프를 담을 컨테이너 (RectTransform)
    public RectTransform graphContainer2; // 그래프를 담을 컨테이너 (RectTransform)
    public GameObject barPrefab; // 막대 그래프의 프리팹 (GameObject)
    public GameObject barPrefab2; // 막대 그래프의 프리팹 (GameObject)
    public float barWidth = 0f; // 막대의 너비
    public float spacing = 50f; // 막대 사이의 간격

    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            treasure_LoadStatsInfo();
        });
    }

    void treasure_LoadStatsInfo(){
        //요소+날짜별
        for(int j=4;j>-1;j--){
            DateTime today = DateTime.Today.AddDays(-j); 
            string dateString = today.ToString("yyyy-MM-dd");
            GetStatsInfo("rate",dateString, j, treasureRate);
            GetStatsInfo("playTime",dateString, j, treasurePlayTime);
            GetStatsInfo("count_eyes",dateString, j, treasureCount_Eyes);
            GetStatsInfo("count_face",dateString, j, treasureCount_Face);
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
        databaseReference.Child("User+"+CurrentUserID).Child(ConstellationName).Child("treasure").Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] =  InfoValue;
                    Debug.Log("treasure+date"+date+"InfoToGet"+InfoToGet+"InfoValue"+InfoValue);
                    Debug.Log("treasure+saveInfoArray[j]"+saveInfoArray[j]+"/j"+j);
                }
                
            }
        });
    }
    public void printInfo(){
        for(int l=0;l<5;l++){
            Debug.Log("treasure Rate:"+treasureRate[l]+"l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("treasure PlayTime:"+treasurePlayTime[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("treasure Count_Eyes:"+treasureCount_Eyes[l]+"/l:"+l);
        }
        for(int l=0;l<5;l++){
            Debug.Log("treasure Count_Face:"+treasureCount_Face[l]+"/l:"+l);
        }

        float[] treasurePlayTimeF = Array.ConvertAll(treasurePlayTime, float.Parse);
        CreateBars(treasurePlayTimeF, new Vector2(0f, 0f));
        float[] treasureRateF = Array.ConvertAll(treasureRate, float.Parse);
        CreateBars2(treasureRateF, new Vector2(0f, 0f));
    }

    public void printEye()
    {
        float[] treasureCount_EyesF = Array.ConvertAll(treasureCount_Eyes, float.Parse);
        CreateBars(treasureCount_EyesF, new Vector2(0f, 0f));
    }

    public void printFace()
    {
        float[] treasureCount_FaceF = Array.ConvertAll(treasureCount_Face, float.Parse);
        CreateBars(treasureCount_FaceF, new Vector2(0f, 0f));
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
            barRect.anchoredPosition = new Vector2(xPos, (yPos + barHeight / 2f) - 10f);// 막대의 위치 설정 (막대의 중심을 그래프 컨테이너의 위쪽으로 향하도록 조정)

            UnityEngine.Debug.Log("1. treasure barHeight:" + barHeight + "/l:" + i);
            UnityEngine.Debug.Log("1. treasure xPos:" + xPos + "/l:" + i);
            UnityEngine.Debug.Log("1. treasure yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
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
            else barHeight = values[i] * graphHeight - 30f;
            // 값을 기준으로 막대의 높이 계산 (0~200 범위를 그래프 컨테이너의 높이 범위로 변환)float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)

            float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)
            barRect.sizeDelta = new Vector2(barWidth, barHeight); // 막대의 크기 설정
            barRect.anchoredPosition = new Vector2(xPos, (yPos + barHeight / 2f) - 10f); // 막대의 위치 설정 (막대의 중심을 그래프 컨테이너의 위쪽으로 향하도록 조정)

            UnityEngine.Debug.Log("2. treasure barHeight:" + barHeight + "/l:" + i);
            UnityEngine.Debug.Log("2. treasure xPos:" + xPos + "/l:" + i);
            UnityEngine.Debug.Log("2. treasure yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
        }
    }
}

