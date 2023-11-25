using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{

    Rigidbody2D rb;
    public GameObject player;
    public Transform spawnPos;
    public static float y1 = 0, x1 = 0;
    bool moveStart = false; //�ڲ� ó���� ĳ���Ͷ� player�� �浹������ ȭ�� �߾��ʿ� ���°��� ��������
    public string Road = ""; //��� �������� ���ϴ��� �ȳ�
    //[SerializeField] float speed = 6f; //스피드

    void Start()
    {
        float y1 = player.GetComponent<RectTransform>().anchoredPosition.y;
        float x1 = player.GetComponent<RectTransform>().anchoredPosition.x;
        //float y1 = player.GetComponent<Transform>().position.y;
        //float x1 = player.GetComponent<Transform>().position.x;
        if (SceneManager.GetActiveScene().name == "TreasureHunt1")
        {
            //x1 = -6.45f;
            //x1 = -8.17f;
            //y1 = -3.29f;
            Road = "Up";
        }
        else if (SceneManager.GetActiveScene().name == "TreasureHunt2" || SceneManager.GetActiveScene().name == "TH2")
        {
            //x1 = 7.31f;
            //y1 = 2.43f;
            Road = "Down";
        }
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
                if (hit.transform.gameObject.name == "startButton" && Player.start_Move == false) //최초로 start버튼 눌렀을때만 반응하도록
                {
                    Click_Start();
                }
            }
        }
    }
    void Click_Start()
    {
        //Debug.Log("rectTransform ��ġ: " + rectTransform.anchoredPosition);
        gameObject.SetActive(false);
        Invoke("WaitForIt",3.0f);
    }

    void WaitForIt()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Right") // ���ΰ����� ���� �ε�������
        {
            Road = "Right";
        }
        else if (other.gameObject.tag == "Down") // ���ΰ����� ���� �ε�������
        {
            Road = "Down";
        }
        else if (other.gameObject.tag == "Left") // ���ΰ����� ���� �ε�������
        {
            Road = "Left";
        }
        else if (other.gameObject.tag == "Up") // ���ΰ����� ���� �ε�������
        {
            Road = "Up";
        }
    }
    public void Moving()
    {
        Debug.Log("ĳ���� �����̴���");
        if (moveStart == false)
        {
            //if (SceneManager.GetActiveScene().name == "TreasureHunt1") { x1 = -6.45f; y1 = -3.29f; }
            //if (SceneManager.GetActiveScene().name == "TreasureHunt1") { x1 = -8.17f; y1 = -3.29f; }
            //else if (SceneManager.GetActiveScene().name == "TreasureHunt2") { x1 = 7.31f; y1 = 2.43f; }
            moveStart = true;
        }
        else if (moveStart == true)
        {
            Debug.Log("현재 위치: " + x1 + y1);
            switch (Road)
            {
                case "Right":
                    x1 += 5;
                    break;
                case "Down":
                    y1 -= 5;
                    break;
                case "Left":
                    x1 -= 5;
                    break;
                case "Up":
                    y1 += 5;
                    break;
                default:
                    break;
            }
            player.GetComponent<RectTransform>().anchoredPosition = new Vector3(x1, y1);
            //player.GetComponent<Transform>().position = new Vector3(x1, y1);
        }
    }

    public void Moving_Stop()
    {
        Debug.Log("Moving Stop");
    }
    
    public void ComeBack()
    {
        Debug.Log("comeBack함수로 접근되었습니다!!");
        Debug.Log("현재 씬은: "+ SceneManager.GetActiveScene().name);
        //this.transform.position = new Vector3(-5.8f, -4.28f, 0);

        Time.timeScale = 0;
        Invoke("Delay", 2.0f); //2초 뒤 함수 실행. 2초 동안 시간 잠깐 멈추기 투명 벽에 부딪혀서 약간의 주의를 주는 느낌
        Time.timeScale = 1;
    }

    public void Delay()
    {
        Time.timeScale = 1;
    }
}
