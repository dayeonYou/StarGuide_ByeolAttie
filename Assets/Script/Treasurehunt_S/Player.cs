using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    //PlayerMove move;
    public GameObject move;
    public static bool start_Move = false;

    public TH_EyeTrackingScript EyeTracking;
    public RectTransform gazePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //move = GameObject.Find("Canvas").transform.Find("collegestudent1").GetComponent<PlayerMove>();
        //move = GameObject.Find("collegestudent3").GetComponent<PlayerMove>();
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
                if (hit.transform.gameObject.name == "startButton")
                {
                    //start_Move = true;
                }
            }
        }

        if (start_Move == true)
        {
            if (EyeTracking.isTracking)
            {
                rb.position = gazePoint.position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "showItem")
        {
            Debug.Log("플레이어가 근처에 있습니다!"); 
        }
        
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("플레이어를 움직입니다~");
            //PlayerMove�� ĳ���� �̵��Լ� ȣ���ϱ�
            //move.Moving();
            move.GetComponent<PlayerMove>().Moving();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "showItem") // ���ΰ����� ���� �ε�������
        {
            //Debug.Log("�÷��̾ �����Դϴ�~");
            //move.Moving();
            move.GetComponent<PlayerMove>().Moving();
        }   
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "showItem")
        {
            Debug.Log("플레이어에게 집중하세요 멀어집니다!!");
            //PlayerMove�� ĳ���� �����Լ� ȣ���ϱ�
            //move.Moving_Stop();
            move.GetComponent<PlayerMove>().Moving_Stop();
        }
    }
}
