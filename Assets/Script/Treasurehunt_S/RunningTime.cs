using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningTime : MonoBehaviour
{
	public bool timerOn = false;
	public static float totalTime = 0f;
	
	void Update()
	{
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭한 좌표값 가져오기
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.name == "startButton")
                {
                    timerOn = true;
                }
            }
        }
        if (timerOn)
		{
            if(Clear_Time.clear_Set != true) //clear 패널이 떴을때는 시간 흐르지 않도록 조절
            {
                //totalTime = PlayerPrefs.GetFloat("TotalTime");
                totalTime += Time.deltaTime;
            }		
		}
	}
}
