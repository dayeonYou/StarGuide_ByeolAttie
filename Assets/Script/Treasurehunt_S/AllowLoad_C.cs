using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllowLoad_C: MonoBehaviour
{
    public TH_EyeTrackingScript EyeTrackingScript;

    //public PlayerMove player; //Player 스크립트의 Delay()함수 참조하기 캐릭터 움직임 멈추기 위해, 투명벽이랑 부딪힌 오브젝트가 플레이어인지 확인용
    public GameObject player;
    //private SpriteRenderer sprite;
    private Image img;
    void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();
        //sprite.enabled = false;
        img = GetComponent<Image>();
        img.enabled = false;
        GameObject.Find("Canvas").transform.Find("Panel_Question").gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //player = GameObject.Find("Canvas").transform.Find("collegestudent").GetComponent<PlayerMove>();//null값 안 들어가게 해줌! 변수 선언 오류 해결!!
        //player = GameObject.Find("Canvas").transform.Find("collegestudent1").gameObject.GetComponent<PlayerMove>();//null값 안 들어가게 해줌! 변수 선언 오류 해결!!
        //player = GameObject.Find("collegestudent3").GetComponent<PlayerMove>();
        if (other.gameObject.tag == "Player") //주인공에게 직접 부딪혔을때
        {
            Debug.Log("이쪽으로 가세요!");
            Debug.Log(this.gameObject.name);
            //sprite.enabled = true;
            img.enabled = true;
            if (this.gameObject.name == "FinishLine")
            {
                EyeTrackingScript.stopTracking();
                GameObject.Find("Canvas").transform.Find("Panel_Question").gameObject.SetActive(true);
                //플레이어 못 움직이게 하기
                //player.Delay(); //Tips. 가끔 함수 is inaccessible due to its protection level 오류 나는데 public으로 선언 안 해줘서임.
                player.GetComponent<PlayerMove>().Delay();
                //GameObject.Find("Canvas").transform.Find("collegestudent").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("Canvas").transform.Find("collegestudent1").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("collegestudent3").transform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 0;//시간 흐름 비율 0으로
            }
        }
        //result += res;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "showItem")
        {
            //sprite.enabled = false;
            img.enabled = false;
            Debug.Log("길 가리기!"); //아이템이 획득되었을때도 이 문구 뜨는것 해결하기!
        }
    }
}
