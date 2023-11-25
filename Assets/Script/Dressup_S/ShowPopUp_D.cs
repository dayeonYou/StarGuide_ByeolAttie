using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp_D : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    void Start()
    {
        //GameObject.Find("Canvas").transform.Find("Panel_GameOver").gameObject.SetActive(false);
        //GameObject.Find("Canvas").transform.Find("Panel_Clear").gameObject.SetActive(false);
    }

    public void Show_D()
    {
        if(obj.CompareTag("clear")) obj.SetActive(true);
        if(obj.CompareTag("gameover")) obj.SetActive(true);
        //Time.timeScale = 0;
    }
}
