using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemEnabled : MonoBehaviour
{
    SpriteRenderer sprite;
    //public Score_TH score; //외부 스크립트의 변수에 접근
    //public PlayerMove player;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.material.color = new Color(59 / 255f, 74 / 255f, 113 / 255f, 0 / 255f);
    }
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
                if (hit.transform.gameObject.name == "startButton" && Player.start_Move == false)
                {
                    click_Start(); //이 줄 밑 줄에 Player.start_Move = true; 하면 함수 호출 후 5개 아이템이 보이기 전에 true로 
                                    //변수 바뀌어서 1개밖에 안 보임.   
                }
            }
        }
    }
    void click_Start()
    {
        //score = GameObject.Find("score").GetComponent<Score_TH>();
        //sprite = gameObject.GetComponent<SpriteRenderer>();
        //sprite.color = Color.white;
        sprite.material.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        Invoke("WaitForIt", 3.0f);
    }
    void WaitForIt()
    {
        //sprite.color = Color.black; //색 배경색으로 바꾸기
        sprite.material.color = new Color(59 / 255f, 74 / 255f, 113 / 255f, 0 / 255f);
        Player.start_Move = true; //플레이어 움직임 시작하는 것을 3초 뒤에 실행해야함.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //sprite.color = Color.white;
        if (other.gameObject.tag == "showItem")
        {
            Debug.Log("아이템 발견!!");
            sprite.material.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            //Debug.Log(this.transform.position);
        }
        if (other.gameObject.tag == "Player") //주인공에게 직접 부딪혔을때
        {
            Debug.Log("아이템을 줍습니다.");
        }
        //result += res;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "showItem")
        {
            //sprite.color = Color.black; //Destroy(this.gameobject)없애진 않고 다시 안 보이게 됨(파란배경색으로 바꾸기)
            sprite.material.color = new Color(59 / 255f, 74 / 255f, 113 / 255f, 0 / 255f);
            Debug.Log("아이템 어디갔지?"); //아이템이 획득되었을때도 이 문구 뜨는것 해결하기!
        }
    }
}
