using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using Firebase;
using Firebase.Database;

public class GetClearData : MonoBehaviour
{
    private DatabaseReference reference = null;
    public string CurrentConstellation;
    public int NumOfClearStars;
    public int NumOfConstellationStars;
    public int NumOfConstellationLines;
    public GameObject[] obj;
    public GameObject[] objL;

    private void Awake() // 클리어한 별들은 빛나도록 초기화
    {
        // 데이터베이스 URI 지정
        FirebaseApp.DefaultInstance.Options.DatabaseUrl =
                    new System.Uri("https://total1.firebaseio.com/");
 
        // 파이어베이스의 메인 참조 얻기
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
 
        // 읽기, 로그로 나옴
        ReadClearData("User+"+CurrentUserID);
        // Debug.Log("User+" + CurrentUserID);
        
    }
    public void ReadClearData(string _dataSet)
    {
        DatabaseReference uiReference = FirebaseDatabase.DefaultInstance.GetReference(_dataSet);

        uiReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot data in snapshot.Children)
                {
                    // 각 자식의 데이터를 IDictionary로 캐스팅
                    IDictionary childData = (IDictionary)data.Value;

                    // "Cons"와 "Info" 노드의 자식들의 키와 값을 가져옴
                    foreach (var key in childData.Keys)
                    {
                        // Debug.Log(key + ": " + childData[key]);
                        if((string)key == CurrentConstellation){
                            // string value = childData[key].ToString();
                            int.TryParse(childData[key].ToString(), out int result);
                            NumOfClearStars = result;
                            Debug.Log("numclear: "+ NumOfClearStars);
                            // NumOfClearStars 값을 변경한 후에 원하는 기능 수행
                            // SomeOtherFunction();
                        }
                    }
                }
            }
        });
    }
    void Start(){
        SomeOtherFunction();
    }
    private void SomeOtherFunction()
    {
        // NumOfClearStars 값이 변경된 후에 수행할 기능을 여기에 추가
        Debug.Log("NumOfClearStars 값이 변경되었습니다. 다른 기능을 수행합니다." + NumOfClearStars);
        for(int i=0;i<NumOfConstellationStars;i++){ //0,1,2,3
            // 오브젝트가 존재하는지 체크
            if(i<NumOfClearStars){//0
                SpriteRenderer spriteRenderer = obj[i].GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(1f,0.3f,0.5f);
                Debug.Log("clearSTAR:" + i);
            }
            else{//1,2,3
                SpriteRenderer spriteRenderer = obj[i].GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(0.5f,0.5f,0.5f);
                Debug.Log("NOTclearSTAR:" + i);
            }
        }
        Debug.Log("NumOfClearStars 값이 변경되었습니다. 다른 기능을 수행합니다.2" + NumOfClearStars);

    }
}
