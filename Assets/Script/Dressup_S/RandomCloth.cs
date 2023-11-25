using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RandomCloth : MonoBehaviour
{
    public GameObject[] prefabs; //찍어낼 게임 오브젝트 종류 배열로 만든 이유는 게임 오브젝트를
                                 //다양하게 찍어내기 위함.
    public GameObject instance;
    public static int hat_F, shirt_F, pant_F, shoes_F = 0; //처음 랜덤으로 뽑힌 색상
    public Transform spawnPoint;
    //RectTransform pos; //UI의 위치 조정
    string name = "";

    public static bool OnStartBtn = false; //start버튼 눌렸는지 확인
    public static bool OnPauseBtn = false; //pause버튼 눌렸는지 확인
    public static int CreateNum = 0; //옷 부위 4개 다 생성됐는지 확인
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
                if (hit.transform.gameObject.name == "startButton" && OnStartBtn == false)
                {
                    if (CreateNum < 4)
                    {
                        GameObject.Find("Canvas").transform.Find("Button_hat").gameObject.SetActive(true);
                        GameObject.Find("Canvas").transform.Find("Button_shirt").gameObject.SetActive(true);
                        GameObject.Find("Canvas").transform.Find("Button_pant").gameObject.SetActive(true);
                        GameObject.Find("Canvas").transform.Find("Button_shoes").gameObject.SetActive(true);
                        SetCloth();
                        WaitForIt();
                        CreateNum++;
                        Debug.Log("옷 생성 : "+ CreateNum);
                    }
                    //else OnStartBtn = true;
                }
            }
        }
        /*
        if(CreateNum >= 4 && SettingCloth == false) {
            Debug.Log("옷 다 입혔습니다! startBtn true로 전환합니다.");
            SettingCloth = true;
            OnStartBtn = true;
        }
        */
    }

    void WaitForIt()
    {
        Debug.Log(transform.position);
        Debug.Log("현재 아이템 색상 : " + instance.name);
        Debug.Log("현재 아이템 tag : " + instance.tag);
        switch (instance.tag)
        {
            case "hat":
                if (GameObject.Find("Hat2_black(Clone)"))
                {
                    Debug.Log("검정 모자");
                    hat_F = 0;
                    instance.tag = "h_0(R)";
                }
                else if (GameObject.Find("Hat2_brown(Clone)"))
                {
                    Debug.Log("갈색 모자");
                    hat_F = 1;
                    instance.tag = "h_1(R)";
                }
                else if (GameObject.Find("Hat2_pink(Clone)"))
                {
                    Debug.Log("분홍 모자");
                    hat_F = 2;
                    instance.tag = "h_2(R)";
                }
                else if (GameObject.Find("Hat2_white(Clone)"))
                {
                    Debug.Log("하양 모자");
                    hat_F = 3;
                    instance.tag = "h_3(R)";
                }
                else if (GameObject.Find("Hat2_blue(Clone)"))
                {
                    Debug.Log("파랑 모자");
                    hat_F = 4;
                    instance.tag = "h_4(R)";
                }
                else if (GameObject.Find("Hat2_red(Clone)"))
                {
                    Debug.Log("빨강 모자");
                    hat_F = 5;
                    instance.tag = "h_5(R)";
                }
                else if (GameObject.Find("Hat2_yellow(Clone)"))
                {
                    Debug.Log("노랑 모자");
                    hat_F = 6;
                    instance.tag = "h_6(R)";
                }
                break;
            case "shirt":
                if (GameObject.Find("Shirt_black(Clone)"))
                {
                    Debug.Log("검정 상의");
                    shirt_F = 0;
                    instance.tag = "s_0(R)";
                }
                else if (GameObject.Find("Shirt_brown(Clone)"))
                {
                    Debug.Log("갈색 상의");
                    shirt_F = 1;
                    instance.tag = "s_1(R)";
                }
                else if (GameObject.Find("Shirt_pink(Clone)"))
                {
                    Debug.Log("분홍 상의");
                    shirt_F = 2;
                    instance.tag = "s_2(R)";
                }
                else if (GameObject.Find("Shirt_white(Clone)"))
                {
                    Debug.Log("하양 상의");
                    shirt_F = 3;
                    instance.tag = "s_3(R)";
                }
                else if (GameObject.Find("Shirt_blue(Clone)"))
                {
                    Debug.Log("파랑 상의");
                    shirt_F = 4;
                    instance.tag = "s_4(R)";
                }
                else if (GameObject.Find("Shirt_red(Clone)"))
                {
                    Debug.Log("빨강 상의");
                    shirt_F = 5;
                    instance.tag = "s_5(R)";
                }
                else if (GameObject.Find("Shirt_yellow(Clone)"))
                {
                    Debug.Log("노랑 상의");
                    shirt_F = 6;
                    instance.tag = "s_6(R)";
                }
                break;
            case "pant":
                if (GameObject.Find("Skirt_black(Clone)"))
                {
                    Debug.Log("검정 하의");
                    pant_F = 0;
                    instance.tag = "p_0(R)";
                }
                else if (GameObject.Find("Skirt_brown(Clone)"))
                {
                    Debug.Log("갈색 하의");
                    pant_F = 1;
                    instance.tag = "p_1(R)";
                }
                else if (GameObject.Find("Skirt_pink(Clone)"))
                {
                    Debug.Log("분홍 하의");
                    pant_F = 2;
                    instance.tag = "p_2(R)";
                }
                else if (GameObject.Find("Skirt_white(Clone)"))
                {
                    Debug.Log("하양 하의");
                    pant_F = 3;
                    instance.tag = "p_3(R)";
                }
                else if (GameObject.Find("Skirt_blue(Clone)"))
                {
                    Debug.Log("파랑 하의");
                    pant_F = 4;
                    instance.tag = "p_4(R)";
                }
                else if (GameObject.Find("Skirt_red(Clone)"))
                {
                    Debug.Log("빨강 하의");
                    pant_F = 5;
                    instance.tag = "p_5(R)";
                }
                else if (GameObject.Find("Skirt_yellow(Clone)"))
                {
                    Debug.Log("노랑 하의");
                    pant_F = 6;
                    instance.tag = "p_6(R)";
                }
                break;
            case "shoes":
                if (GameObject.Find("Shoes_black(Clone)"))
                {
                    Debug.Log("검정 신발");
                    shoes_F = 0;
                    instance.tag = "ss_0(R)";
                }
                else if (GameObject.Find("Shoes_brown(Clone)"))
                {
                    Debug.Log("갈색 신발");
                    shoes_F = 1;
                    instance.tag = "ss_1(R)";
                }
                else if (GameObject.Find("Shoes_pink(Clone)"))
                {
                    Debug.Log("분홍 신발");
                    shoes_F = 2;
                    instance.tag = "ss_2(R)";
                }
                else if (GameObject.Find("Shoes_white(Clone)"))
                {
                    Debug.Log("하양 신발");
                    shoes_F = 3;
                    instance.tag = "ss_3(R)";
                }
                else if (GameObject.Find("Shoes_blue(Clone)"))
                {
                    Debug.Log("파랑 신발");
                    shoes_F = 4;
                    instance.tag = "ss_4(R)";
                }
                else if (GameObject.Find("Shoes_red(Clone)"))
                {
                    Debug.Log("빨강 신발");
                    shoes_F = 5;
                    instance.tag = "ss_5(R)";
                }
                else if (GameObject.Find("Shoes_yellow(Clone)"))
                {
                    Debug.Log("노랑 신발");
                    shoes_F = 6;
                    instance.tag = "ss_6(R)";
                }
                break;
            default:
                Debug.Log("옷 색상이 선택되지 않았습니다");
                break;
        }
    }
    private Vector3 GetPosition()
    {
        float posX = 0.0f; float posY = 0.0f;
        string tag = this.gameObject.tag;
        Debug.Log(tag);

        switch (tag)
        { //hat(x=0.73, y=0.63) shirt(x=0.85, y=1) pant(x=0.77,y=0.65) shoes(x=0.81,y=1)
            case "hat":
                name = "hat1";
                break;
            case "shirt":
                name = "shirt1";
                break;
            case "pant":
                name = "pant1";
                break;
            case "shoes":
                name = "shoes1";
                break;
        }
        Vector3 spawnPos = new Vector3(posX, posY, 0);
        //Vector3 spawnPos = new Vector3(0, 0, 0);
        //anchoredRect.anchoredPosition = new Vector2(AnchoredX, AnchoredY);
        return spawnPos;
        //return pos;
    }

    private void SetCloth()
    {
        int rand_cloth = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab = prefabs[rand_cloth];
        Vector3 spawnPos = GetPosition(); //위치결정함수
        Debug.Log(spawnPos);
        instance = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation, GameObject.Find("Canvas").transform.Find(name).gameObject.transform);
    }
}
