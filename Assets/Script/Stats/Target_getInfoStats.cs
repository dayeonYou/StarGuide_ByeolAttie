using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine.UI;

public class Target_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayTarget = { "gTime", "sTime", "count_Eyes", "count_Face" };

    string[] TargetGTime = new string[5];
    string[] TargetSTime = new string[5];
    string[] TargetCount_Eyes = new string[5];
    string[] TargetCount_Face = new string[5];

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
            TargetStar_LoadStatsInfo();

        });
    }

    void TargetStar_LoadStatsInfo()
    {
        //요소+날짜별
        for (int j = 4; j > -1; j--)
        {
            DateTime today = DateTime.Today.AddDays(-j);
            string dateString = today.ToString("yyyy-MM-dd");
            GetStatsInfo("gTime", dateString, j, TargetGTime);
            GetStatsInfo("sTime", dateString, j, TargetSTime);
            GetStatsInfo("count_eyes", dateString, j, TargetCount_Eyes);
            GetStatsInfo("count_face", dateString, j, TargetCount_Face);
        }

    }
    void Update()
    {
        if (databaseReference != null)
        {
            // Debug.Log("reference is not null2.");
        }
        else
        {
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Debug.Log("reference is null2.");

        }

    }
    void GetStatsInfo(string InfoToGet, string date, int j, string[] saveInfoArray)
    {

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        if (databaseReference != null)
        {
            // Debug.Log("reference is not null2.");
        }
        else
        {
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        databaseReference.Child("User+" + CurrentUserID).Child(ConstellationName).Child("Target").Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] = InfoValue;
                    Debug.Log("Target+date" + date + "InfoToGet" + InfoToGet + "InfoValue" + InfoValue);
                    Debug.Log("Target+" + InfoToGet + saveInfoArray[j] + "/j" + j);
                }

            }
        });
    }
    public void printInfo()
    {
        for (int i = 4; i > -1; i--)
        {
            Debug.Log("Target TargetGTime:" + TargetGTime[i] + "/l:" + i);
        }
        for (int i = 4; i > -1; i--)
        {
            Debug.Log("Target TargetSTime:" + TargetSTime[i] + "/l:" + i);
        }
        for (int i = 4; i > -1; i--)
        {
            Debug.Log("Target TargetCount_Eyes:" + TargetCount_Eyes[i] + "/l:" + i);
        }
        for (int i = 4; i > -1; i--)
        {
            Debug.Log("Target TargetCount_Face:" + TargetCount_Face[i] + "/l:" + i);
        }

        float[] TargetGTimeFloat = Array.ConvertAll(TargetGTime, float.Parse);
        CreateBars(TargetGTimeFloat, new Vector2(0f, 0f));

        float[] TargetSTimeFloat = Array.ConvertAll(TargetSTime, float.Parse);
        CreateBars2(TargetSTimeFloat, new Vector2(0f, 0f));

    }

    public void printEye()
    {
        float[] TargetCount_EyesF = Array.ConvertAll(TargetCount_Eyes, float.Parse);
        CreateBars(TargetCount_EyesF, new Vector2(0f, 0f));
        float[] TargetCount_FaceF = Array.ConvertAll(TargetCount_Face, float.Parse);
        CreateBars2(TargetCount_FaceF, new Vector2(0f, 0f));
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

            Debug.Log("1. Target barHeight:" + barHeight + "/l:" + i);
            Debug.Log("1. Target xPos:" + xPos + "/l:" + i);
            Debug.Log("1. Target yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
        }
    }

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
            else barHeight = (values[i] / 200f) * graphHeight;
            // 값을 기준으로 막대의 높이 계산 (0~200 범위를 그래프 컨테이너의 높이 범위로 변환)float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)

            float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)
            barRect.sizeDelta = new Vector2(barWidth, barHeight); // 막대의 크기 설정
            barRect.anchoredPosition = new Vector2(xPos, (yPos + barHeight / 2f) - 20f); // 막대의 위치 설정 (막대의 중심을 그래프 컨테이너의 위쪽으로 향하도록 조정)

            Debug.Log("2. Target barHeight:" + barHeight + "/l:" + i);
            Debug.Log("2. Target xPos:" + xPos + "/l:" + i);
            Debug.Log("2. Target yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
        }
    }
}

