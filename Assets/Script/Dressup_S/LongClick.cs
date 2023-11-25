using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongClick : MonoBehaviour
{
    /*
    EyeTrakingScript EyeTrakingScript;
    public GameObject progressBar;
    private Image itemSlider;
    float isLongClickTime = 1.0f;
    float updateTime = 0.0f;
    public static bool closetShow = false;
    void Start()
    {
        //itemSlider = progressBar.transform.Find("LoadingBar").GetComponent<Image>();
        itemSlider = GameObject.Find("LoadingBar").GetComponent<Image>();
        itemSlider.fillAmount = 0.0f;
    }
    void Update()
    {
        if (updateTime > isLongClickTime)
        {
            updateTime = 0.0f;
            itemSlider.fillAmount = 1.0f;
        }
        if (NotShow_Cloth.ButtonClicked)
        {
            //updateTime = 1.0f;
            updateTime = updateTime + Time.deltaTime;
            itemSlider.fillAmount = 1.0f - (Mathf.Lerp(0, 100, updateTime / isLongClickTime) / 100);
        }
        if (NotShow_Cloth.ButtonClicked != true)
        {
            Reset();
            if (closetShow == false && NotShow_Cloth.click_hat >= 1 && NotShow_Cloth.click_shirt >= 1 &&
            NotShow_Cloth.click_pant >= 1 && NotShow_Cloth.click_shoes >= 1)
            {
                //Reset();
                Debug.Log("������ ��ü�� �� ���������ϴ�!!");
                GameObject.Find("Canvas").transform.Find("Panel_closet").gameObject.SetActive(true);
                closetShow = true;
            }
        }
    
    }

    void Reset()
    {
        //Debug.Log("���� �� �ʱ�ȭ!");
        itemSlider.fillAmount = 0.0f; //��ư ������ ������ ����bar�� ���ۺκ����� ���ؾ���.
    }

    */
}
