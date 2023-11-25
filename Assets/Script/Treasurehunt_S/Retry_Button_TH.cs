using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //이 코드 없으면 Image를 찾을 수 없다는 오류남.
using UnityEngine.SceneManagement;

public class Retry_Button_TH : MonoBehaviour
{
    StopStart_TH S_S;
    RandomScene randomScene;
    RunningTime runtime; //RunningTime 스크립트의 Destroy_Time()함수 참조하기
    public Sprite[] sprites;

    public void OnClick_Retry() //재시도
    {
        //string scene = SceneManager.GetActiveScene().name; //지금 작동중인 씬을 가져올수 있음
        //SceneManager.LoadScene(scene);
        randomScene = GameObject.Find("RandomScene").GetComponent<RandomScene>();
        randomScene.Scene();
        //runtime.Destroy_Time();
        Reset();
        Time.timeScale = 1;//시간 흐름 비율 1으로
        GameObject.Find("Canvas").transform.Find("Panel_Question").gameObject.SetActive(false);
    }

    public void OnClick_Replay() //재개 버튼 누르면 게임 다시 재개
    {
        S_S = GameObject.Find("pauseButton").GetComponent<StopStart_TH>();
        if (S_S.pauseOn)
        {
            Debug.Log("재개!!");
            GameObject.Find("pauseButton").GetComponent<Image>().sprite = sprites[0];
            Time.timeScale = 1.0f;//시간 흐름 비율 1로
            GameObject.Find("Panel_pause").SetActive(false);
        }
        //StopStart.pauseOn = !StopStart.pauseOn;//불 값 반전
        S_S.pauseOn = !S_S.pauseOn;
    }

    public void OnClick_Back() //뒤로가기 버튼 누르면 스테이지 창으로 가기
    {
        SceneManager.LoadScene("TreasureHunt1"); //스테이지 창으로 scene 변환. 잠시 게임 시작화면으로 테스트
        Time.timeScale = 1; //시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
        Reset();
    }

    public void Reset()
    {
        Score_TH.gold = 0; Score_TH.jewel = 0;
        Score_TH.crown = 0; Score_TH.coin = 0;
        Score_TH.score = 0; Score_TH.Max_score = 0;
        RandomSpawn.gold = 0; RandomSpawn.crown = 0;
        RandomSpawn.jewel = 0; RandomSpawn.coin = 0;
        PlayerMove.x1 = 0; PlayerMove.y1 = 0;
        RunningTime.totalTime = 0;
        Clear_Time.clear_Set = false;
        Player.start_Move = false;
    }
}
