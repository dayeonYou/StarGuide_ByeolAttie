using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//GetComponent<Image>하기 위해서 추가!!
//안하면 The type or namespace name 'Image' could not be found오류남

public class StopStart : MonoBehaviour
{
    public Sprite[] sprites;
    public bool pauseOn = false;
    public GameObject pause_obj;
    public void Start() //게임 시작하고 나서는 팝업창 가려놔야함.
    {
        //pause_obj = GameObject.Find("Panel_pause");
        //pause_obj.gameObject.SetActive(false);
    }

    public void Show() //일시정지 버튼 눌렀을때 팝업창과 함께 멈춤
    {
        if(RandomCloth.OnStartBtn == true){
            pause_obj.gameObject.SetActive(true);
            if (!pauseOn)
            {//일시정지 중이 아니면 일시정지
                GameObject.Find("pauseButton").GetComponent<Image>().sprite = sprites[1];
                Debug.Log("�Ͻ�����!!");
                Time.timeScale = 0;//시간 흐름 비율 0으로
            }
            pauseOn = !pauseOn;//불 값 반전
        }      
    }
}
