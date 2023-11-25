using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 시 추가
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class MakeAnswer : MonoBehaviour
{
    public Find_Tracking Find_Tracking;
    public DisplayRound DisplayRound;
    public Find_DetectCollision Find_DetectCollision;

    GenerateCup generateCupScript;
    DisplayNumOfAnswer displayNumOfAnswerScript;
    DatabaseReference databaseReference;

    public GameObject[] cups;
    public Vector3[] positions;
    public int randomCupIndex;
    public GameObject ball;
    public GameObject ballPrefab; // ball 프리팹

    public Button stBtn;
    public Sprite abledImage;
    public GameObject WrongAnswerImage;

    public float delayTime = 1.0f;

    public float fadeDuration = 0.3f; // 페이드 인/아웃에 걸리는 시간

    private float currentAlpha = 1.0f; // 현재 알파값
    private float targetAlpha = 0.0f; // 목표 알파값

    private Renderer objectRenderer; // 렌더러 컴포넌트
    private GameObject targetObject; // 타겟 오브젝트

    private Image imageH;
    public Sprite newImage;
    public GameObject heartObject;
    public static int indexHeart = 3;

    public TextMeshProUGUI textMeshPro_overMessage;
    public TextMeshProUGUI textMeshPro_clearMessage;

    public GameObject btnB;
    public GameObject btnR;
    public GameObject squareWindowGame;
    
    public string ConstellationName;
    public string realAnswer;

    // Start is called before the first frame update
    void Start()
    {
        generateCupScript=FindObjectOfType<GenerateCup>();
        displayNumOfAnswerScript=FindObjectOfType<DisplayNumOfAnswer>();
        //stBtn = GameObject.Find("stBtn").GetComponent<Button>();
        
        // cups = generateCupScript.cups;
        // positions = generateCupScript.positions;
       
        indexHeart = 3;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });

    }

    public IEnumerator displayAnswerFst(string targetObjectName)
    {
        targetObject = GameObject.Find(targetObjectName);
        if (targetObject != null)
        {
            objectRenderer = targetObject.GetComponent<Renderer>();
        }
        else
        {
            Debug.LogError("Target object not found!");
            yield break; // 반복 종료
        }

        // 타겟 오브젝트가 없으면 스크립트 동작 중지
        if (targetObject == null)
        {
            yield break; // 반복 종료
        }

        int blinkCount = 0;

        while (blinkCount < 3)
        {
            // 페이드 아웃 효과를 위해 알파값을 0으로 조정
            targetAlpha = 0.0f;
            float elapsedTime = 0.0f;

            // 페이드 아웃
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / fadeDuration);
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, t);

                Material material = objectRenderer.material;
                Color color = material.color;
                color.a = currentAlpha;
                material.color = color;

                yield return null;
            }

            // 페이드 인 효과를 위해 알파값을 1로 조정
            targetAlpha = 1.0f;
            elapsedTime = 0.0f;

            // 페이드 인
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / fadeDuration);
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, t);

                Material material = objectRenderer.material;
                Color color = material.color;
                color.a = currentAlpha;
                material.color = color;

                yield return null;
            }

            blinkCount++;
        }
    }

    public int makeBall(){
        // 공 랜덤한 컵 안에 생성
        randomCupIndex = UnityEngine.Random.Range(0, cups.Length);
        if(randomCupIndex < cups.Length) {
            //Vector3 ballPosition = cups[randomCupIndex].transform.position + new Vector3(0, -1, 10);
            //ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
            Debug.Log("ball" + randomCupIndex);
            //렌더링 순서 변경
            //Renderer ballRenderer = ball.GetComponent<Renderer>();
            //Renderer cupRenderer = cups[randomCupIndex].GetComponent<Renderer>();
            //ballRenderer.sortingOrder = cupRenderer.sortingOrder + 1;
            return randomCupIndex;
        }
        else{
            Debug.Log("randomCupIndex"+randomCupIndex + "cups.Length"+ cups.Length);
            return 10;
        }
    }
    // public void HideObject() {
    //     ball.GetComponent<Renderer>().enabled = false;
    // }
    public bool checkAnswer(string selectedAnswer){
        realAnswer= "Cup" + randomCupIndex.ToString();
        if(realAnswer==selectedAnswer){
            displayNumOfAnswerScript.OnAnswerCorrectly();
            return true;
        }
        else{
            GameObject objHeart = GameObject.Find("Heart"+indexHeart);
            if (objHeart != null)
            {
                imageH = objHeart.GetComponent<Image>();
            }
            else
            {
                Debug.Log("Cannot find GameObject with the name");
            }

            if ((imageH != null))
            {
                imageH.sprite = newImage;
                if(indexHeart > 0) indexHeart--;
                Debug.Log("imageH is not null."+indexHeart);
                
                WrongAnswerImage.SetActive(true);
                StartCoroutine(SetObjectActive());
                
                //게임 오버
                if(indexHeart == 0){
                    Debug.Log("game over!!");
                    AddDataToDatabase3();
                    squareWindowGame.SetActive(true);
                    textMeshPro_overMessage.gameObject.SetActive(true);
                    textMeshPro_clearMessage.gameObject.SetActive(false);

                    btnB.SetActive(true);
                    btnR.SetActive(true);
                    Find_Tracking.stopTracking();

                    stBtn.interactable = false;
                    Image image = stBtn.GetComponent<Image>();
                    if (image != null && abledImage != null)
                    {
                        image.sprite = abledImage;
                    }
                    else{
                        Debug.Log("abled Image");
                    }
                    
                }
                Debug.Log("indexHeart"+indexHeart);
            }
            else{
                Debug.Log("heart sprite renderer is null");
            }
            return false;
        }
    }
    public void DestroyObject() {
        Destroy(ball);
    }
    public void HideObject2() {
        Invoke("HideObject", 0.7f);
    }
    // void update(){
    //     if(stBtn.interactable==false) Invoke("HideObject", delayTime);
    // }
    public void AddDataToDatabase3(){
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

        int count_faceValue = Find_Tracking.count_face;
        int count_eyesValue = Find_Tracking.count_eyes;
        int count_click = DisplayRound.clickCount + displayNumOfAnswerScript.numOfAnswer;
        // int numOfAnswer = count_click - 3;
        int numOfAnswer = displayNumOfAnswerScript.numOfAnswer;
        double rate = (double)numOfAnswer / count_click;
        float stareTime = Find_DetectCollision.sec;

        Debug.Log("count1: "+count_faceValue);
        Debug.Log("count2: "+count_eyesValue);
        Debug.Log("count click: "+count_click);
        Debug.Log("numofanswer: "+numOfAnswer);
        Debug.Log("stareTime: "+stareTime);
        // Debug.Log("numofanswer2: "+numOfAnswer2);
        Debug.Log("rate: "+rate);
        
        // int count_faceValue = 5;
        // int count_eyesValue = 6;
        // double rate = 7.1;

        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        Debug.Log("currentuserid"+CurrentUserID);

        string path_count_faceValue = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/count_face";
        string path_count_eyesValue = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/count_eyes";
        string path_count_rate = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/rate";
        string path_count_stareTime = "User+"+CurrentUserID+"/"+ConstellationName+"/FindStar/"+dateString+"/stareTime";

        databaseReference.Child(path_count_faceValue).SetValueAsync(count_faceValue).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("count_faceValue : 추가 성공! + "+count_faceValue);
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

        WrongAnswerImage.SetActive(false);
    }

}
