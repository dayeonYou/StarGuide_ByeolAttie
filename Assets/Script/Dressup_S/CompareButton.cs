using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class CompareButton : MonoBehaviour
{
    //hat <-> shirt , pant <-> shoes
    Heart_gaze heart_Image;
    ShowPopUp_D clear;
    ClosetShow closet; //closet이 order이 더 높아서 팝업창이 떴을때 옷장 패널에 가려진다. 

    DatabaseReference databaseReference;
    public string ConstellationName;
    public EyeTrakingScript eyeTrakingScript;

    public GameObject CorrectAnswerImage; //정답을 틀리면 이미지 잠깐 띄우기
    public GameObject WrongAnswerImage;
    GameObject obj; //무슨 이미지를 비활성화 해야하는지 지정하는 변수 
    IEnumerator SetObjectActive(){
        yield return new WaitForSeconds(3f);

        if(obj == CorrectAnswerImage ) CorrectAnswerImage.SetActive(false);
        else WrongAnswerImage.SetActive(false);
    }
    void WaitForIt(){ //Invoke 3초 뒤에 WaitForIt()함수를 실행시키기 때문에 3초동안 이미지 보인 뒤 바로 없어지고 LoadScene호출됨.
        if(obj == CorrectAnswerImage ) CorrectAnswerImage.SetActive(false);
        else {
            WrongAnswerImage.SetActive(false);
            heart_Image.Gazing();
        }
    }

    void Start()
    {
        eyeTrakingScript=FindObjectOfType<EyeTrakingScript>();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });

        switch(Heart_gaze.heartnum){
            case 3:
                heart_Image = GameObject.Find("heart3").GetComponent<Heart_gaze>();
                Debug.Log("heart 세 개 : "+heart_Image);
                break;
            case 2:
                heart_Image = GameObject.Find("heart2").GetComponent<Heart_gaze>();
                Debug.Log("heart 두 개 : "+heart_Image);
                break;
            case 1:
                heart_Image = GameObject.Find("heart1").GetComponent<Heart_gaze>();
                Debug.Log("heart 한 개 : "+heart_Image);
                break;
        }
        //heart_Image = GameObject.FindWithTag("heart").GetComponent<Heart_gaze>();
        clear = GameObject.Find("Canvas").transform.Find("Panel_Clear").GetComponent<ShowPopUp_D>();
        closet = GameObject.Find("Canvas").transform.Find("Panel_closet").GetComponent<ClosetShow>();
    }

    public void Button_On()
    {
        Time.timeScale = 1;
        if (NotShow_Cloth.click_hat > 0 && NotShow_Cloth.click_shirt > 0 && NotShow_Cloth.click_pant > 0 && NotShow_Cloth.click_shoes > 0)
        {
            Scoring();
        }
    }

    public void Scoring()
    {
        switch(Heart_gaze.heartnum){
            case 3:
                heart_Image = GameObject.Find("heart3").GetComponent<Heart_gaze>();
                Debug.Log("heart 세 개 : "+heart_Image);
                break;
            case 2:
                heart_Image = GameObject.Find("heart2").GetComponent<Heart_gaze>();
                Debug.Log("heart 두 개 : "+heart_Image);
                break;
            case 1:
                heart_Image = GameObject.Find("heart1").GetComponent<Heart_gaze>();
                Debug.Log("heart 한 개 : "+heart_Image);
                break;
        }
        //heart_Image = GameObject.FindWithTag("heart").GetComponent<Heart_gaze>();
        clear = GameObject.Find("Canvas").transform.Find("Panel_Clear").GetComponent<ShowPopUp_D>();
        closet = GameObject.Find("Canvas").transform.Find("Panel_closet").GetComponent<ClosetShow>();

        Debug.Log("현재 hat 종류: " + ChooseCloth.h_before);
        Debug.Log("현재 shirt 종류: " + ChooseCloth.s_before);
        Debug.Log("현재 pant 종류: " + ChooseCloth.p_before);
        Debug.Log("현재 shoes 종류: " + ChooseCloth.ss_before);
        Debug.Log("정답 hat 종류: " + RandomCloth.hat_F);
        Debug.Log("정답 shirt 종류: " + RandomCloth.shirt_F);
        Debug.Log("정답 pant 종류: " + RandomCloth.pant_F);
        Debug.Log("정답 shoes 종류: " + RandomCloth.shoes_F);

        if (ChooseCloth.h_before == RandomCloth.hat_F && ChooseCloth.s_before == RandomCloth.shirt_F &&
            ChooseCloth.p_before == RandomCloth.pant_F && ChooseCloth.ss_before == RandomCloth.shoes_F)
        {
            Score.score += 4;
            Score.Max_score += 4;
            Debug.Log("정답입니다!! 정말 잘하시네요~");
            heart_Image.Reset(); //�ٽ��ϴ� ��� �������� �ʱ�ȭ �ʿ�
            obj = CorrectAnswerImage;
            obj.SetActive(true); //정답 맞추면 정답 이미지 3초동안 재생
            Invoke("WaitForIt", 3.0f);
            //obj.SetActive(false);
            //StartCoroutine(SetObjectActive());
            closet.Not_ShowCloset();
            clear.Show_D();     
            
            int count_faceValue = eyeTrakingScript.count_face;
            int count_eyesValue = eyeTrakingScript.count_eyes;
            float clickTime = eyeTrakingScript.clickTime;

            int scoreDB = Score.score;
            int Max_scoreDB = Score.Max_score;
            double rate = (double)scoreDB / Max_scoreDB;
            
            DateTime today = DateTime.Today;
            string dateString = today.ToString("yyyy-MM-dd");
            Debug.Log(dateString);
            
            string CurrentUserID = "";
            CurrentUserID = PlayerPrefs.GetString("UserID");
            string path_count_faceValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Dressup/"+dateString+"/count_face";
            string path_count_eyesValue = "User+"+CurrentUserID+"/"+ConstellationName+"/Dressup/"+dateString+"/count_eyes";
            string path_clickTime = "User+"+CurrentUserID+"/"+ConstellationName+"/Dressup/"+dateString+"/clickTime";
            string path_count_rate = "User+"+CurrentUserID+"/"+ConstellationName+"/Dressup/"+dateString+"/rate";
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
                    Debug.Log("추가 성공!");
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
            databaseReference.Child(path_clickTime).SetValueAsync(clickTime).ContinueWithOnMainThread(task =>
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
        }
        else
        {
            if (ChooseCloth.h_before == RandomCloth.hat_F)
            {
                Plus();
                Debug.Log("���� �����ϴ�!!");
            }
            if (ChooseCloth.s_before == RandomCloth.shirt_F)
            {
                Plus();
                Debug.Log("���� �����ϴ�!!");
            }
            if (ChooseCloth.p_before == RandomCloth.pant_F)
            {
                Plus();
                Debug.Log("���� �����ϴ�!!");
            }
            if (ChooseCloth.ss_before == RandomCloth.shoes_F)
            {
                Plus();//�������� ���ᰳ���� �˾ƾ� ��賾 �� ����.
                Debug.Log("�Ź� �����ϴ�!!");
            }
            Score.Max_score += 4;
            Debug.Log("�ƽ��Ե� �����Դϴ٤Ф�"); //�� ���� ���ϸ� ����� �پ��
            Debug.Log(Heart_gaze.heartnum);
            Debug.Log("���� ���� : " + Score.score + "/" + Score.Max_score);
            obj = WrongAnswerImage;
            obj.SetActive(true); //정답 틀리면 틀린 이미지 3초동안 재생
            Invoke("WaitForIt", 3.0f);
            //obj.SetActive(false);
            //StartCoroutine(SetObjectActive());
            //heart_Image.Gazing();
        }
    }
    void Plus()
    {
        Score.score++;
    }
}
