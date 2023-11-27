using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clear_Time : MonoBehaviour
{
    public TextMeshProUGUI countDown;
    public int minute = 0;
    public static bool clear_Set = false;

    public void Start()
    {
        countDown = GetComponent<TextMeshProUGUI>();
    }
    
    private int TimerCalc()
    {
        minute = (int)RunningTime.totalTime;
        return minute;
    }

    public void Update()
    {
        if(clear_Set == true)
        {
            minute = TimerCalc();
            if (minute >= 60)
            {
                minute = minute / 60;
                countDown.text = "게임 클리어까지 " + minute + "분 걸렸어!" + "\n" + "이제 다음 단계로 나아가보자!";

            }
            else
            {
                countDown.text = "게임 클리어까지 " + minute + "초  걸렸어!" + "\n" + "이제 다음 단계로 나아가보자!";

            }
        }
    }
}
