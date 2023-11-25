using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text UI구성 요소 사용하기 위해 이 네임스페이스 추가해야함.
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro; //TextMeshPro의 텍스트를 변경하려면 필요
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class Score_TH : MonoBehaviour
{
    public static int gold, crown, jewel, coin = 0;
    TextMeshProUGUI score_text_gold, score_text_crown, score_text_jewel, score_text_coin;
    ShowPopUp_TH clear; //Panel_Q의 Show()함수 참조하기
    RandomScene randomScene; //RandomScene 스크립트의 Scene()함수 참조하기
    RunningTime runtime; //RunningTime 스크립트의 Destroy_Time()함수 참조하기
    //GameObject obj;
    private bool correct_wrong = false;
    //string x = coinAmount.ToString(); //정수 coinAmount를 string으로 변환

    public static int score = 0;
    public static int Max_score = 0 ; //??????? ????? ?????? ????
    DatabaseReference databaseReference;
    public string ConstellationName;
    public Clear_Time clearTime;
    public TH_EyeTrackingScript eyeTrakingScript;
    public GameObject CorrectAnswerImage; //������ Ʋ���� �̹��� ��� ����
    public GameObject WrongAnswerImage;

    GameObject obj; //무슨 이미지를 비활성화 해야하는지 지정하는 변수 

    IEnumerator SetObjectActive(){
        yield return new WaitForSeconds(3f);
        if(obj == CorrectAnswerImage ) CorrectAnswerImage.SetActive(false);
        else WrongAnswerImage.SetActive(false);
        
    }

    void WaitForIt(){//Invoke 3초 뒤에 WaitForIt()함수를 실행시키기 때문에 3초동안 이미지 보인 뒤 바로 없어지고 LoadScene호출됨.
        if(obj == CorrectAnswerImage ) CorrectAnswerImage.SetActive(false);
        else {
            WrongAnswerImage.SetActive(false);
            randomScene.Scene();
        }
    }
    void Start()
    {
        eyeTrakingScript=FindObjectOfType<TH_EyeTrackingScript>();

        score_text_gold = GameObject.FindWithTag("Score_gold").GetComponent<TextMeshProUGUI>();
        score_text_crown = GameObject.FindWithTag("Score_crown").GetComponent<TextMeshProUGUI>();
        score_text_jewel = GameObject.FindWithTag("Score_jewel").GetComponent<TextMeshProUGUI>();
        score_text_coin = GameObject.FindWithTag("Score_coin").GetComponent<TextMeshProUGUI>();
        //clear = GameObject.Find("Canvas").transform.Find("Panel_Clear").GetComponent<Panel_C>();
        clear = GameObject.Find("Canvas").transform.Find("Panel_Clear").GetComponent<ShowPopUp_TH>();
        randomScene = GameObject.Find("RandomScene").GetComponent<RandomScene>();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void ClickBtn()
    {
        //방금 클릭한 게임 오브젝트를 가져와서 저장
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        Debug.Log("버튼 클릭"+clickObject);
        Debug.Log("클릭한 아이템 이름: " + clickObject.transform.parent.name);
        switch (clickObject.transform.parent.name)
        {
            case "goldNum":
                if (clickObject.tag == "Plus") Plus_Num("goldNum");
                else if (clickObject.tag == "Minus") Minus_Num("goldNum");
                score_text_gold.text = gold.ToString(); //코인획득시 ItemEnabled.cs의 1씩 증가되는 값을 문자로 변환하여 text변수에 저장.
                break;
            case "crownNum":
                if (clickObject.tag == "Plus") Plus_Num("crownNum");
                else if (clickObject.tag == "Minus") Minus_Num("crownNum");
                score_text_crown.text = crown.ToString(); //코인획득시 ItemEnabled.cs의 1씩 증가되는 값을 문자로 변환하여 text변수에 저장.
                break;
            case "jewelNum":
                if (clickObject.tag == "Plus") Plus_Num("jewelNum");
                else if (clickObject.tag == "Minus") Minus_Num("jewelNum");
                score_text_jewel.text = jewel.ToString(); //코인획득시 ItemEnabled.cs의 1씩 증가되는 값을 문자로 변환하여 text변수에 저장.
                break;
            case "coinNum":
                if (clickObject.tag == "Plus") Plus_Num("coinNum");
                else if (clickObject.tag == "Minus") Minus_Num("coinNum");
                score_text_coin.text = coin.ToString(); //코인획득시 ItemEnabled.cs의 1씩 증가되는 값을 문자로 변환하여 text변수에 저장.
                break;
            default:
                Debug.Log("부모 오브젝트가 없습니다");
                break;
        }
    }
    public void Plus_Num(string clickPlusItem)
    {
        switch (clickPlusItem)
        {
            case "goldNum":
                gold++;
                break;
            case "crownNum":
                crown++;
                break;
            case "jewelNum":
                jewel++;
                break;
            case "coinNum":
                coin++;
                break;
            default:
                Debug.Log("잘못 클릭되었습니다");
                break;
        }
        Debug.Log("갯수 올립니다!!");
    }
    public void Minus_Num(string clickMinusItem)
    {
        switch (clickMinusItem)
        {
            case "goldNum":
                gold--;
                break;
            case "crownNum":
                crown--;
                break;
            case "jewelNum":
                jewel--;
                break;
            case "coinNum":
                coin--;
                break;
            default:
                Debug.Log("잘못 클릭되었습니다");
                break;
        }
        Debug.Log("갯수 내립니다!!");
    }

    public void Correct_Score()
    {
        runtime = GameObject.Find("Time").GetComponent<RunningTime>();
        Debug.Log("???: " + gold + " ???: " + crown + " ???: " + jewel + " ????: " + coin);
        Debug.Log("???: " + RandomSpawn.gold + " ???: " + RandomSpawn.crown + " ???: " + RandomSpawn.jewel + " ????: " + RandomSpawn.coin);
        if (RandomSpawn.gold == gold && RandomSpawn.crown == crown && RandomSpawn.jewel == jewel && RandomSpawn.coin == coin)
        {
            Debug.Log("다 맞혔어요~ 기억력이 정말 좋으시네요");
            //clear panel ???? ?????
            clear.Show_TH();
            obj = CorrectAnswerImage;
            obj.SetActive(true); //정답 맞추면 정답 이미지 3초동안 재생
            Invoke("WaitForIt", 3.0f);
            //obj.SetActive(false);
            //StartCoroutine(SetObjectActive());
            Clear_Time.clear_Set = true;
            score += 4;
            Max_score += 4;
            Debug.Log("???? ???? : " + score + "/" + Max_score);
            GameObject.Find("Canvas").transform.Find("Panel_Question").gameObject.SetActive(false); //??????? ??????
            //RunningTime.totalTime = 0;


            int count_faceValue = eyeTrakingScript.count_face;
            int count_eyesValue = eyeTrakingScript.count_eyes;
            double playTime = clearTime.minute;

            double rate = (double)score / Max_score;

            DateTime today = DateTime.Today;
            string dateString = today.ToString("yyyy-MM-dd");
            Debug.Log(dateString);

            string CurrentUserID = "";
            CurrentUserID = PlayerPrefs.GetString("UserID");
            string path_count_faceValue = "User+"+CurrentUserID+"/"+ConstellationName+"/treasure/"+dateString+"/count_face";
            string path_count_eyesValue = "User+"+CurrentUserID+"/"+ConstellationName+"/treasure/"+dateString+"/count_eyes";
            string path_playTime = "User+"+CurrentUserID+"/"+ConstellationName+"/treasure/"+dateString+"/playTime";
            string path_count_rate = "User+"+CurrentUserID+"/"+ConstellationName+"/treasure/"+dateString+"/rate";
            if(databaseReference!=null){   
                Debug.Log("reference is not null2.");
            }
            else{
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("reference is null2.");
            }
            databaseReference.Child(path_count_faceValue).SetValueAsync(count_faceValue).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("�߰� ����!");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError(" �߰� ����: " + task.Exception);
                }
            });
            databaseReference.Child(path_count_eyesValue).SetValueAsync(count_eyesValue).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log(" �߰� ����!");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError(" �߰� ����: " + task.Exception);
                }
            });
            databaseReference.Child(path_playTime).SetValueAsync(playTime).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log(" �߰� ����!");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError(" �߰� ����: " + task.Exception);
                }
            });
            databaseReference.Child(path_count_rate).SetValueAsync(rate).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log(" �߰� ����!");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError(" �߰� ����: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.Log("틀린 경우 " + correct_wrong);
            Debug.Log("???: " + gold + " ???: " + crown + " ???: " + jewel + " ????: " + coin);
            if (correct_wrong == false)
            {
                if (RandomSpawn.gold == gold)
                {
                    score++;
                    Debug.Log("??? ???????!!");
                }
                if (RandomSpawn.crown == crown)
                {
                    score++;
                    Debug.Log("??? ???????!!");
                }
                if (RandomSpawn.jewel == jewel)
                {
                    score++;
                    Debug.Log("??? ???????!!");
                }
                if (RandomSpawn.coin == coin)
                {
                    score++;//???????? ???????? ???? ??? ?? ????.
                    Debug.Log("???? ???????!!");
                }
                Max_score += 4;
                correct_wrong = true;
                Debug.Log("???? ???? : " + score + "/" + Max_score);
            }
            obj = WrongAnswerImage;
            obj.SetActive(true); //정답 틀리면 틀린 이미지 3초동안 재생
            Invoke("WaitForIt", 3.0f);
            //obj.SetActive(false);
            //StartCoroutine(SetObjectActive());
            //randomScene.Scene();
            //Time.timeScale = 1;//?��? ?? ???? 1????
            //SceneManager.LoadScene("TreasureHunt"); //minute?? static???? ??????? ????? ?��? ??? ???? ????
        }
        gold = 0; jewel = 0;
        crown = 0; coin = 0;
        RandomSpawn.gold = 0; RandomSpawn.crown = 0;
        RandomSpawn.jewel = 0; RandomSpawn.coin = 0;
        PlayerMove.x1 = 0;  PlayerMove.y1 = 0;
        Player.start_Move = false;
        //Clear_Time.clear_Set = false;
    }
}
