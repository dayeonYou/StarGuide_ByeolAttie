using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotShow_Cloth1 : MonoBehaviour
{
    private float clickTime = 0.0f; //클릭 중인 시간
    private float longClickTime = 1.0f;
    public static bool isLongClick = false;//클릭 중인지 판단
    //public LongClick click;
    public static bool ButtonClicked = false;
    public static int click_hat, click_shirt, click_pant, click_shoes = 0;
    //public static bool closetShow = false;
    //버튼 클릭이 시작했을 때
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void ButtonDown()
    {
        isLongClick = true;
        ButtonClicked = true;
    }

    //버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        isLongClick = false;
        ButtonClicked = false;
        print(clickTime);
        //클릭 중인 시간이 최소 클릭시간 이상이라면 
        if (clickTime >= longClickTime)
        {
            switch (transform.gameObject.name)
            {
                case "Button_hat":
                    NotShow_hat();
                    click_hat++;
                    Debug.Log("click_hat : " + click_hat);
                    break;
                case "Button_shirt":
                    NotShow_shirt();
                    click_shirt++;
                    Debug.Log("click_shirt : " + click_shirt);
                    break;
                case "Button_pant":
                    NotShow_pant();
                    click_pant++;
                    Debug.Log("click_pant : " + click_pant);
                    break;
                case "Button_shoes":
                    NotShow_shoes();
                    click_shoes++;
                    Debug.Log("click_shoes : " + click_shoes);
                    break;
                default:
                    Debug.Log("body버튼이 잘못 클릭되었습니다!");
                    break;
            }
        }
    }
    void Update()
    {
        //클릭 중이라면
        if (isLongClick)
        {
            //클릭시간 측정
            clickTime += Time.deltaTime;
        }
        /*
        if (ButtonClicked != true)
        {
            if (closetShow == false && click_hat >= 1 && click_shirt >= 1 &&
            click_pant >= 1 && click_shoes >= 1)
            {
                Debug.Log("아이템 전체가 다 가려졌습니다!!");
                GameObject.Find("Canvas").transform.Find("Panel_closet").gameObject.SetActive(true);
                closetShow = true;
            }
        }
        */
        //클릭 중이 아니라면
        else
        {
            //클릭시간 초기화
            //this.gameObject.GetComponent<Image>().material.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0 / 255f);
            clickTime = 0;
        }
    }
    public void NotShow_hat()
    {
        if (GameObject.Find("Hat2_black(Clone)"))
        {
            GameObject.Find("Hat2_black(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Hat2_brown(Clone)"))
        {
            GameObject.Find("Hat2_brown(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Hat2_pink(Clone)"))
        {
            GameObject.Find("Hat2_pink(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Hat2_white(Clone)"))
        {
            GameObject.Find("Hat2_white(Clone)").gameObject.SetActive(false);
        }
    }
    public void NotShow_shirt()
    {
        if (GameObject.Find("Shirt_black(Clone)"))
        {
            GameObject.Find("Shirt_black(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shirt_brown(Clone)"))
        {
            GameObject.Find("Shirt_brown(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shirt_pink(Clone)"))
        {
            GameObject.Find("Shirt_pink(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shirt_white(Clone)"))
        {
            GameObject.Find("Shirt_white(Clone)").gameObject.SetActive(false);
        }
    }
    public void NotShow_pant()
    {
        if (GameObject.Find("Skirt_black(Clone)"))
        {
            GameObject.Find("Skirt_black(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Skirt_brown(Clone)"))
        {
            GameObject.Find("Skirt_brown(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Skirt_pink(Clone)"))
        {
            GameObject.Find("Skirt_pink(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Skirt_white(Clone)"))
        {
            GameObject.Find("Skirt_white(Clone)").gameObject.SetActive(false);
        }
    }
    public void NotShow_shoes()
    {
        if (GameObject.Find("Shoes_black(Clone)"))
        {
            GameObject.Find("Shoes_black(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shoes_brown(Clone)"))
        {
            GameObject.Find("Shoes_brown(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shoes_pink(Clone)"))
        {
            GameObject.Find("Shoes_pink(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shoes_white(Clone)"))
        {
            GameObject.Find("Shoes_white(Clone)").gameObject.SetActive(false);
        }
    }
}
