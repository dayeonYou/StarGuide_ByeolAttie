using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    public void Bt_T_Click()
    {
        //RunningTime.totalTime = 0;
        if (SceneManager.GetActiveScene().name == "GameStart")
        {
            RunningTime.totalTime = 0;
            SceneManager.LoadScene("TreasureHunt1");
        }
        else if (SceneManager.GetActiveScene().name == "TreasureHunt1")
        {
            RunningTime.totalTime = 0;
            SceneManager.LoadScene("TreasureHunt1");
        }
        else if (SceneManager.GetActiveScene().name == "TreasureHunt2")
        {
            RunningTime.totalTime = 0;
            SceneManager.LoadScene("TreasureHunt2");
        }
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }

    public void Bt_D_Click()
    {
        RunningTime.totalTime = 0;
        SceneManager.LoadScene("Heart3");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_1_Click()
    {
        SceneManager.LoadScene("Aquarius_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_2_Click()
    {
        SceneManager.LoadScene("Aries_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_3_Click()
    {
        SceneManager.LoadScene("Cancer_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_4_Click()
    {
        SceneManager.LoadScene("Capricorn_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_5_Click()
    {
        SceneManager.LoadScene("Gemini");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_6_Click()
    {
        SceneManager.LoadScene("Leo_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_7_Click()
    {
        SceneManager.LoadScene("Libra_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_8_Click()
    {
        SceneManager.LoadScene("Pisces_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_9_Click()
    {
        SceneManager.LoadScene("Sagittarius_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_10_Click()
    {
        SceneManager.LoadScene("Scorpio_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_11_Click()
    {
        SceneManager.LoadScene("Taurus_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
    public void Bt_12_Click()
    {
        SceneManager.LoadScene("Virgo_Constellation");
        Time.timeScale = 1;//시간 흐름 비율 1으로. 이 코드 제외하면 시간이 멈춰서 씬 전환 후 애니메이션 실행 x
    }
}
