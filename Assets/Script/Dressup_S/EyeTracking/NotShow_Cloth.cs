using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotShow_Cloth : MonoBehaviour, IPointerClickHandler
{
    public DetectCollision DetectCollision;
    public EyeTrakingScript EyeTrakingScript;

    public static int click_hat, click_shirt, click_pant, click_shoes = 0;

    public static bool closetShow = false; //옷장이 한번만 보이게 하기 위함.(update함수는 계속 호출하기 때문)
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (DetectCollision.Touch)
        {
            switch (this.gameObject.name)
            {
                case "Button_hat":
                    NotShow_hat();
                    click_hat++;
                    //Debug.Log("click_hat : " + click_hat);
                    break;
                case "Button_shirt":
                    NotShow_shirt();
                    click_shirt++;
                    //Debug.Log("click_shirt : " + click_shirt);
                    break;
                case "Button_pant":
                    NotShow_pant();
                    click_pant++;
                    //Debug.Log("click_pant : " + click_pant);
                    break;
                case "Button_shoes":
                    NotShow_shoes();
                    click_shoes++;
                    //Debug.Log("click_shoes : " + click_shoes);
                    break;
                default:
                    //Debug.Log("body��ư�� �߸� Ŭ���Ǿ����ϴ�!");
                    break;
            }
            this.gameObject.SetActive(false);
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
        else if (GameObject.Find("Hat2_blue(Clone)"))
        {
            GameObject.Find("Hat2_blue(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Hat2_red(Clone)"))
        {
            GameObject.Find("Hat2_red(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Hat2_yellow(Clone)"))
        {
            GameObject.Find("Hat2_yellow(Clone)").gameObject.SetActive(false);
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
        else if (GameObject.Find("Shirt_blue(Clone)"))
        {
            GameObject.Find("Shirt_blue(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shirt_red(Clone)"))
        {
            GameObject.Find("Shirt_red(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shirt_yellow(Clone)"))
        {
            GameObject.Find("Shirt_yellow(Clone)").gameObject.SetActive(false);
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
        else if (GameObject.Find("Skirt_blue(Clone)"))
        {
            GameObject.Find("Skirt_blue(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Skirt_red(Clone)"))
        {
            GameObject.Find("Skirt_red(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Skirt_yellow(Clone)"))
        {
            GameObject.Find("Skirt_yellow(Clone)").gameObject.SetActive(false);
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
        else if (GameObject.Find("Shoes_blue(Clone)"))
        {
            GameObject.Find("Shoes_blue(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shoes_red(Clone)"))
        {
            GameObject.Find("Shoes_red(Clone)").gameObject.SetActive(false);
        }
        else if (GameObject.Find("Shoes_yellow(Clone)"))
        {
            GameObject.Find("Shoes_yellow(Clone)").gameObject.SetActive(false);
        }
    }
}
