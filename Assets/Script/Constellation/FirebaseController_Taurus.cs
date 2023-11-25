using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseController_Taurus : MonoBehaviour
{
    DatabaseReference databaseReference;
    public string ConstellationName;
    float[] colorSet = {0f, 1f, 2f}; // {} 3개의 색(RGB) 세트 정하기
    float[] RandomStarSet = new float[3]; //1f 할거 정하기
    int RandomStar = 1;
    private void Start()
    {
        
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            // Load and deactivate game objects based on Firebase data
            LoadAndDeactivateGameObjects();

        });

    }
    
    private void LoadAndDeactivateGameObjects()
    {
        // Dictionary 생성 및 초기화
        Dictionary<int, int> LineDictionary = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 2 },
            { 3, 5 },
            { 4, 9 },
            { 5, 12 }
        };
        // Dictionary 생성 및 초기화
        Dictionary<int, int> SmallStarDictionary = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 1 },
            { 3, 3 },
            { 4, 5 },
            { 5, 7 }
        };

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        // CurrentUserID = bQMkmHlOM6VYYVYv5VCXnBXQSh13
        databaseReference.Child("User+"+CurrentUserID).Child("Constellation").Child(ConstellationName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    int count = int.Parse(snapshot.Value.ToString());
                    if(count == -1){
                        int elementCount = LineDictionary.Count;
                        count = elementCount;
                    }
                    for (int i = 1; i <= count; i++)
                    {
                        GameObject obj = GameObject.Find("Star" + i.ToString());
                        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                        obj.GetComponent<ClearL1>().SetClickable(true);
                        
                        RandomStar = (int)ChooseRandomNumber(RandomStarSet); // RGB 세트의 0,1,2 요소 중에 1f 할 요소 정하기
                        for(int k=0;k<3;k++){
                            // Debug.Log("RandomStar 1f:"+RandomStar);
                            if(RandomStar != k){
                                float[] RandomValueSet = {0.3f, 0.5f};
                                float RandomValue = ChooseRandomNumber(RandomValueSet); // 1f가 아닌 나머지 2개 요소의 색 정하기 
                                colorSet[k] = RandomValue;
                            }
                            else{
                                colorSet[k] = 1f;
                            }
                        }
                        
                        spriteRenderer.color = new Color(colorSet[0],colorSet[1],colorSet[2]);
                        // spriteRenderer.color = new Color(1f,0.3f,0.5f);
                        Debug.Log("clearSTAR:" + i);
                        Debug.Log("a"+i); 
                    }
                    if(count != LineDictionary.Count){
                        GameObject NextStar = GameObject.Find("Star" + (count+1).ToString());
                        NextStar.GetComponent<ClearL1>().SetClickable(true);
                    }
                    for (int i = count+2; i <= LineDictionary.Count; i++)
                    {
                        GameObject obj = GameObject.Find("Star" + i.ToString());
                        // 클릭 가능한 상태를 false로 설정 (클릭 x)
                        obj.GetComponent<ClearL1>().SetClickable(false);
                    }
                    // 딕셔너리에 접근
                    int LineValue = LineDictionary[count]; // key 2에 해당하는 값 가져옴
                    for (int i = 1; i <= LineValue; i++)
                    {
                        GameObject obj = GameObject.Find("L" + i.ToString());

                        if (obj != null)
                        {
                           obj.SetActive(false);
                           Debug.Log("clearLINE:" + i);
                        }
                    }
                    // 딕셔너리에 접근
                    int SmallStarValue = SmallStarDictionary[count]; // key 2에 해당하는 값 가져옴
                    for (int i = 1; i <= SmallStarValue; i++)
                    {
                        GameObject obj = GameObject.Find("smallStar" + i.ToString());

                        if (obj != null)
                        {
                           obj.SetActive(false);
                           Debug.Log("clearSMALL:" + i);

                        }
                    }
                    // for (int i = 1; i < count; i++)
                    // {
                    //     GameObject obj = GameObject.Find("L" + i.ToString());
                    //     // Debug.Log("clearLine:" + i);

                    //     if (obj != null)
                    //     {
                    //        obj.SetActive(false);
                    //     }
                    //     // Debug.Log("a"+i);
                    // }
                    //지금 실행해야하는 레벨 조금 밝은 회색으로 할지 결정
                    // GameObject objNext = GameObject.Find("Star" + (count+1).ToString());
                    // if (objNext != null)
                    // {
                    //     SpriteRenderer spriteRenderer = objNext.GetComponent<SpriteRenderer>();
                    //     spriteRenderer.color = new Color(0.5f,0.5f,0.5f);

                    // }
                    
                }
            }
        });
    }
    float ChooseRandomNumber(float[] numbers){
        int randomIndex = Random.Range(0,numbers.Length);
        return numbers[randomIndex];
    }
}
