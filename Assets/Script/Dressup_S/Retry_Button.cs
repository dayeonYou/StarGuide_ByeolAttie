using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Retry_Button : MonoBehaviour
{
    StopStart S_S;
    public Sprite[] sprites;
    public void OnClick_Retry() //재시도 pause버튼 눌렀을때 현재씬을 다시 시도할 수 있게 함.
    {
        string scene = SceneManager.GetActiveScene().name; //지금 작동중인 씬을 가져올수 있음
        SceneManager.LoadScene(scene);
        Reset();
        Time.timeScale = 1;//시간 흐름 비율 1으로
    }

    public void OnClick_Next_Clear() //재시도 clear창에서 버튼 눌렀을때 현재씬을 다시 시도할 수 있게 함.(test��.���� �������� ���� ����)
    {
        SceneManager.LoadScene("Heart3");
        Reset();
        Time.timeScale = 1;//시간 흐름 비율 1으로
        Heart_gaze.heartnum = 3;
    }

    public void OnClick_Replay() //재개 버튼 누르면 게임 다시 재개
    {
        S_S = GameObject.Find("pauseButton").GetComponent<StopStart>();
        if (S_S.pauseOn)
        {
            Debug.Log("�簳!!");
            GameObject.Find("pauseButton").GetComponent<Image>().sprite = sprites[0];
            Time.timeScale = 1.0f;//시간 흐름 비율 1으로
            GameObject.Find("Panel_pause").SetActive(false);
            RandomCloth.OnPauseBtn = true;
        }
        //StopStart.pauseOn = !StopStart.pauseOn;//불 값 반전
        S_S.pauseOn = !S_S.pauseOn;
    }

    public void OnClick_Back() //뒤로가기 버튼 누르면 스테이지 창으로 가기
    {
        Reset();
        SceneManager.LoadScene("Heart3"); //스테이지 창으로 scene 변환. 잠시 Heart3으로 테스트
        Time.timeScale = 1;//시간 흐름 비율 1으로
        Heart_gaze.heartnum = 3;

    }

    public void Reset()
    { //다른 씬으로 넘어가기 전에 body옷 부위 클릭 수를 0으로 초기화, 활성화된 옷장 다시 가려놓기
        //처음에 closet가려놓기(씬이 바꼈을때 여전히 closet보이는 경우땜에)
        NotShow_Cloth.closetShow = false;
        Heart_gaze.heartnum = 3;
        ChooseCloth.hat = 0; ChooseCloth.shirt = 0;
        ChooseCloth.pant = 0; ChooseCloth.shoes = 0;
        RandomCloth.hat_F = 0; RandomCloth.shirt_F = 0;
        RandomCloth.pant_F = 0; RandomCloth.shoes_F = 0;
        NotShow_Cloth.click_hat = 0; NotShow_Cloth.click_shirt = 0;
        NotShow_Cloth.click_pant = 0; NotShow_Cloth.click_shoes = 0;
        Score.score = 0;    Score.Max_score = 0;
        RandomCloth.OnStartBtn = false;
        RandomCloth.CreateNum = 0;
        RandomCloth.OnStartBtn = false; RandomCloth.OnPauseBtn = false;
    }
}
