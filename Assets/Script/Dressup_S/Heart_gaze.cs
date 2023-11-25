using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System;

public class Heart_gaze : MonoBehaviour
{
    public static int heartnum = 3;
    //public Sprite[] sprites;
    DatabaseReference databaseReference;
    public string ConstellationName;
    public EyeTrakingScript eyeTrakingScript;
    ShowPopUp_D gameOver;
    ClosetShow closet_shut; //closet이 order이 더 높아서 팝업창이 떴을때 옷장 패널에 가려진다. 

    void Start(){
        eyeTrakingScript=FindObjectOfType<EyeTrakingScript>();

        gameOver = GameObject.Find("Canvas").transform.Find("Panel_GameOver").GetComponent<ShowPopUp_D>();
        closet_shut = GameObject.Find("Canvas").transform.Find("Panel_closet").GetComponent<ClosetShow>();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void Gazing()
    {
        Debug.Log("함수 집입----------------");
        Reset();
        heartnum--;
        //HeartDown();
        if (heartnum >= 1)
        {
            SceneManager.LoadScene("Heart3");
            Time.timeScale = 1;
            //UnityEngine.UIElements.ProgressBar.closetShow = false;
        }
        else
        {
            Debug.Log("~~~~~GameOver~~~~~");
            gameOver.Show_D();
            closet_shut.Not_ShowCloset();

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
    }

    public void Reset()
    { //다른 씬으로 넘어가기 전에 body옷 부위 클릭 수를 0으로 초기화, 활성화된 옷장 다시 가려놓기
        //GameObject.Find("Canvas").transform.Find("Panel_closet").gameObject.SetActive(false);//처음에 closet가려놓기(씬이 바꼈을때 여전히 closet보이는 경우땜에)
        //LongClick.closetShow = false;
        NotShow_Cloth.closetShow = false;
        ChooseCloth.hat = 0;    ChooseCloth.shirt = 0;
        ChooseCloth.pant = 0;   ChooseCloth.shoes = 0;
        RandomCloth.hat_F = 0; RandomCloth.shirt_F = 0;
        RandomCloth.pant_F = 0; RandomCloth.shoes_F = 0;
        NotShow_Cloth.click_hat = 0; NotShow_Cloth.click_shirt = 0;
        NotShow_Cloth.click_pant = 0; NotShow_Cloth.click_shoes = 0;
        RandomCloth.OnStartBtn = false; RandomCloth.OnPauseBtn = false;
        RandomCloth.CreateNum = 0;
    }
    /* //씬 전환 때문에 이미지가 바뀌지 않음(내 경우는)
    void HeartDown()
    {
        Image imageComponent = GetComponent<Image>();
        Sprite newImage = Resources.Load<Sprite>("5.Image/Common/heart2");
        imageComponent.sprite = newImage;
        heartnum--;
    }
    */
    void Update()
    {
        if (heartnum == 0)
        {
            Destroy(GameObject.Find("heart3"));
            Destroy(GameObject.Find("heart2"));
            Destroy(GameObject.Find("heart1"));
        }
        if (heartnum == 1)
        {
            Destroy(GameObject.Find("heart3"));
            Destroy(GameObject.Find("heart2"));
        }
        if (heartnum == 2) Destroy(GameObject.Find("heart3"));        
    }
}
