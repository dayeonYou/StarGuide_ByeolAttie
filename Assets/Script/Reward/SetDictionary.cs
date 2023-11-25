using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
public class SetDictionary : MonoBehaviour
{
    DatabaseReference databaseReference;
    public FirebaseReward firebaseRewardInstance;
    // private bool isScriptAInitialized = false;
    private Dictionary<string, int> ClearConditionDictionary = new Dictionary<string, int>
    {
        { "Aquarius", 3},
        { "Pieces", 5},
        { "Aries", 5},
        { "Taurus", 5},
        { "Gemini", 5},
        { "Cancer" ,5},
        { "Leo" ,3},
        { "Virgo", 5},
        { "Libra", 5},
        { "Scorpius", 4},
        { "Sagittarius", 5},
        { "Capricornus", 5}
    };
    private static Dictionary<string, int> ClearCountDictionary = new Dictionary<string, int>();
    public Dictionary<string, bool> comparisonResult = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            LoadAndDeactivateReward();
        });
    }
    private void LoadAndDeactivateReward()
    {
        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        string objectName = gameObject.name;
        // Debug.Log("objectName: "+objectName);
        // CurrentUserID = bQMkmHlOM6VYYVYv5VCXnBXQSh13
        // 딕셔너리의 키를 배열처럼 가져와서 사용
        string[] ConstellationsArray = new string[ClearConditionDictionary.Count];
        ClearConditionDictionary.Keys.CopyTo(ConstellationsArray, 0);

        // 키 배열 출력
        foreach (string key in ConstellationsArray)
        {
            Debug.Log("Key: " + key);

            databaseReference.Child("User+"+CurrentUserID).Child("Constellation").Child(key).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        int ClearCount = int.Parse(snapshot.Value.ToString());
                        // 초기화된 딕셔너리 사용
                        int ClearCondition = ClearConditionDictionary[objectName];
                        Debug.Log("Value at key 'object': " + ClearCondition);
                        Debug.Log("CLEARCOUNT at key : " + ClearCount);
        
                        // 딕셔너리에 값 추가
                        ClearCountDictionary.Add(key, ClearCount);
                        // 딕셔너리 값 사용 (모든 인스턴스가 공유)
                        // int value = ClearCountDictionary[key];
                        // Debug.Log("Value at key: " + value);
                        // makeBooleanDictionary();
                    }
                    else{
                        Debug.Log("notFound!");
                    }
                    
                }
                else{
                    Debug.Log("notYet!");
                }
            });
        }
        
    }
    private void makeBooleanDictionary(){
        // 두 딕셔너리의 값을 비교하여 새로운 딕셔너리에 저장
        foreach (var key in ClearConditionDictionary.Keys)
        {
            bool MeetTheCondition = (ClearConditionDictionary[key] == ClearCountDictionary[key]);
            comparisonResult.Add(key, MeetTheCondition);
        }

        // 비교 결과 출력
        foreach (var result in comparisonResult)
        {
            Debug.Log(result.Key + ": " + result.Value);
        }
    }
}
