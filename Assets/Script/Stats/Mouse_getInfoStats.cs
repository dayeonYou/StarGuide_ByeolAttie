using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine.UI;
using TMPro;

public class Mouse_getInfoStats : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    string[] InfoToGetArrayMouse = {"count","rTime","count_eyes","count_face"}; 

    string[] MouseCount = new string[5]; 
    string[] MouseRTime = new string[5]; 
    string[] MouseCount_Eyes = new string[5]; 
    string[] MouseCount_Face = new string[5];

    public RectTransform graphContainer; // 그래프를 담을 컨테이너 (RectTransform)
    public RectTransform graphContainer2; // 그래프를 담을 컨테이너 (RectTransform)
    public GameObject barPrefab; // 막대 그래프의 프리팹 (GameObject)
    public GameObject barPrefab2; // 막대 그래프의 프리팹 (GameObject)
    public float barWidth = 0f; // 막대의 너비
    public float spacing = 300f; // 막대 사이의 간격

    public TextMeshProUGUI originalValueTextObj;

    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            Mouse_LoadStatsInfo();
        });
    }

    void Mouse_LoadStatsInfo(){
        //요소+날짜별
        for(int j=4;j>-1;j--){
            DateTime today = DateTime.Today.AddDays(-j); 
            string dateString = today.ToString("yyyy-MM-dd");
            GetStatsInfo("count",dateString, j, MouseCount);
            GetStatsInfo("rTime",dateString, j, MouseRTime);
            GetStatsInfo("count_eyes",dateString, j, MouseCount_Eyes);
            GetStatsInfo("count_face",dateString, j, MouseCount_Face);
        }
    
    }
    void update(){
        if(databaseReference!=null){   
            // Debug.Log("reference is not null2.");
        }
        else{
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Debug.Log("reference is null2.");
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
            // Debug.Log("reference is null2.");
        }
        databaseReference.Child("User+"+CurrentUserID).Child(ConstellationName).Child("Mouse").Child(date).Child(InfoToGet).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string InfoValue = snapshot.Value.ToString();
                    saveInfoArray[j] =  InfoValue;
                    Debug.Log("Mouse+date"+date+"InfoToGet"+InfoToGet+"InfoValue"+InfoValue);
                    Debug.Log("Mouse+saveInfoArray[j]"+saveInfoArray[j]+"/j"+j);
                }
                
            }
        });
    }

    public void printInfo()
    {
        float[] MouseCountF = Array.ConvertAll(MouseCount, float.Parse);
        CreateBars2(MouseCountF, new Vector2(0f, 0f));
        float[] MouseRTimeF = Array.ConvertAll(MouseRTime, float.Parse);
        CreateBars(MouseRTimeF, new Vector2(0f, 0f));
    }

    public void printEye()
    {
        float[] MouseCount_EyesF = Array.ConvertAll(MouseCount_Eyes, float.Parse);
        CreateBars(MouseCount_EyesF, new Vector2(0f, 0f));
        
    }

    public void printFace()
    {
        float[] MouseCount_FaceF = Array.ConvertAll(MouseCount_Face, float.Parse);
        CreateBars(MouseCount_FaceF, new Vector2(0f, 0f));
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

            Debug.Log("1. Mouse barHeight:" + barHeight + "/l:" + i);
            Debug.Log("1. Mouse xPos:" + xPos + "/l:" + i);
            Debug.Log("1. Mouse yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
            barImage.color = new Color32(255, 192, 0, 255);

            // 'value' 오브젝트 복사하기
            TextMeshProUGUI valueTextObj = Instantiate(originalValueTextObj, bar.transform, false);

            float TxPos = position.x + reverseIndex * barWidth - 10f;  // x축 위치 계산
                                                                       // 텍스트 위치를 막대 위로 설정하고, 텍스트 내용을 해당 막대의 값으로 설정
            valueTextObj.transform.localPosition = new Vector2(TxPos / 2, (yPos + barHeight / 2) + 60f);
            valueTextObj.text = values[i].ToString();
        }
    }

    // 카운트 세기
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
            else barHeight = (values[i] / 100f) * graphHeight;
            // 값을 기준으로 막대의 높이 계산 (0~200 범위를 그래프 컨테이너의 높이 범위로 변환)float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)

            float yPos = position.y; // 막대의 y축 위치 계산 (1로 고정)
            barRect.sizeDelta = new Vector2(barWidth, barHeight); // 막대의 크기 설정
            barRect.anchoredPosition = new Vector2(xPos, (yPos + barHeight / 2f) - 20f); // 막대의 위치 설정 (막대의 중심을 그래프 컨테이너의 위쪽으로 향하도록 조정)

            Debug.Log("2. Mouse barHeight:" + barHeight + "/l:" + i);
            Debug.Log("2. Mouse xPos:" + xPos + "/l:" + i);
            Debug.Log("2. Mouse yPos:" + yPos + "/l:" + i);

            Image barImage = bar.GetComponent<Image>(); // 막대의 Image 컴포넌트 가져옴
            barImage.color = new Color32(255, 192, 0, 255);

            // 'value' 오브젝트 복사하기
            TextMeshProUGUI valueTextObj = Instantiate(originalValueTextObj, bar.transform, false);

            float TxPos = position.x + reverseIndex * barWidth - 10f;  // x축 위치 계산
                                                                       // 텍스트 위치를 막대 위로 설정하고, 텍스트 내용을 해당 막대의 값으로 설정
            valueTextObj.transform.localPosition = new Vector2(TxPos / 2, (yPos + barHeight / 2) + 60f);
            valueTextObj.text = values[i].ToString();
        }
    }
}
