using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear_Time : MonoBehaviour
{
    public Text countDown;
    public int minute = 0;
    public static bool clear_Set = false;

    public void Start()
    {
        countDown = GetComponent<Text>();
    }
    
    private int TimerCalc()
    {
        minute = (int)RunningTime.totalTime / 60;
        return minute;
    }

    public void Update()
    {
        if(clear_Set == true)
        {
            minute = TimerCalc();
            countDown.text = "클리어 시간 : " + minute + " 분 걸렸습니다!!";
            //Time.timeScale = 0; //시간 흐름 비율 0으로
        } 
    }
}
