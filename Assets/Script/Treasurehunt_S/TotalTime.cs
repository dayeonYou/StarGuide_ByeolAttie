using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalTime : MonoBehaviour
{
    public Text countDown;
    public int minute = 0;

	void Update()
	{
       countDown.text = "경과 시간 : " + TimerCalc();   
	}
	private string TimerCalc()
	{
		minute = (int)RunningTime.totalTime / 60;
		return minute + " 분 지났어요!! ";
	}
}
