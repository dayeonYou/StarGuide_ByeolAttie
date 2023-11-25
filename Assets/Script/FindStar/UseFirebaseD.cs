using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using Firebase;
using Firebase.Database;
//using Firebase.Unity.Editor;
using Firebase.Extensions; 
public class UseFirebaseD : MonoBehaviour
{
    private DatabaseReference reference;

    public class UserInfo
    {
        public string UserID = "";
        public int TestGameResult = 12;
        public int TestGameResult2 = 345;
 
        public UserInfo(string _UserID)
        {
            this.UserID = _UserID;
        }
 
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["TestGameResult"] = this.TestGameResult;
            dic["TestGameResult2"] = this.TestGameResult2;
            return dic;
        }
    }
 
 
    // private DatabaseReference reference = null;
 
 
    private void Start()
    {
        // 데이터베이스 URI 지정
        // FirebaseApp.DefaultInstance.Options.DatabaseUrl =
                    // new System.Uri("https://total1.firebaseio.com/");
 
        // 파이어베이스의 메인 참조 얻기
        // reference = FirebaseDatabase.DefaultInstance.RootReference;
 
        // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // Firebase Realtime Database에 대한 참조 얻기
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });

        // 추가
        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        CreateUserWithJson("USERID", new UserInfo(CurrentUserID));
        CreateUserWithPath("USERID2", new UserInfo("userID2"));
        
        // 갱신, 갱신하고 싶을 때 사용
        UpdateUserInfo("USERID2", new UserInfo("USERID2!!"));
        
        // 제거, 제거하고 싶은 데이터의 키
        RemoveUserInfo("USERID3");
 
        // 읽기, 로그로 나옴
        ReadUserInfos("users");
    }
 
    public void CreateUserWithJson(string _userID, UserInfo _userInfo)
    {
        string json = JsonUtility.ToJson(_userInfo);
        reference.Child("users").Child(_userID).SetRawJsonValueAsync(json);
    }
 
    public void CreateUserWithPath(string _userID, UserInfo _userInfo)
    {
        reference.Child("users").Child(_userID).Child("name").SetValueAsync(_userInfo.UserID);
    }
 
    public void UpdateUserInfo(string _userID, UserInfo _userInfo)
    {
        reference.Child("users").Child(_userID).UpdateChildrenAsync(_userInfo.ToDictionary());
    }
 
    public void ReadUserInfos(string _dataSet)
    {
        // 특정 데이터셋의 DB 참조 얻기
        DatabaseReference uiReference = FirebaseDatabase.DefaultInstance.GetReference(_dataSet);
 
        uiReference.GetValueAsync().ContinueWith(
            task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
 
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        // JSON 자체가 딕셔너리 기반
                        IDictionary userInfo = (IDictionary)data.Value;
                        Debug.Log("ID: " + userInfo["UserID"]);
                    }
                }
            });
    }
 
    public void RemoveUserInfo(string _userID)
    {
        reference.Child("users").Child(_userID).RemoveValueAsync();
    }
}
