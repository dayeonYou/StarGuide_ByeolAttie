using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;

public class DisplayEmailPwInfo : MonoBehaviour
{
    public GameObject textEmailInfo; // 텍스트프로메쉬 텍스트 오브젝트
    public GameObject textPwInfo;
    TextMeshProUGUI textMeshProEmailInfo;
    TextMeshProUGUI textMeshProPwInfo;
    private bool hiddenOrNot;
    void Start()
    {
        hiddenOrNot = false;
        string savedPassword = PlayerPrefs.GetString("UserPassword");
        string savedEmail = PlayerPrefs.GetString("UserEmail");
        textMeshProEmailInfo = textEmailInfo.GetComponent<TextMeshProUGUI>();
        textMeshProPwInfo = textPwInfo.GetComponent<TextMeshProUGUI>();
        int pwLen = savedPassword.Length;
        string hiddenPw="";
        for(int i=0;i<pwLen;i++){
            hiddenPw+="*";
        }
        textMeshProEmailInfo.text = savedEmail;
        textMeshProPwInfo.text = hiddenPw;

    }
    public void ClickPwText(){
        hiddenOrNot = !hiddenOrNot;
        StartCoroutine(DisplayRealPw());
    }

    public System.Collections.IEnumerator DisplayRealPw()
    {
        float duration = 0.5f;

        string savedPassword = PlayerPrefs.GetString("UserPassword");
        textMeshProPwInfo = textPwInfo.GetComponent<TextMeshProUGUI>();

        if(hiddenOrNot){
            textMeshProPwInfo.DOFade(0f, duration);
            yield return new WaitForSeconds(duration);
            textMeshProPwInfo.text = savedPassword;
        }
        else{
            textMeshProPwInfo.DOFade(0f, duration);
            yield return new WaitForSeconds(duration);
            int pwLen = savedPassword.Length;
            string hiddenPw="";
            for(int i=0;i<pwLen;i++){
                hiddenPw+="*";
            }
            textMeshProPwInfo.text = hiddenPw;
        }
        textMeshProPwInfo.DOFade(1f, duration);

    }
    // public void ClickPwText(){
    //     string savedPassword = PlayerPrefs.GetString("UserPassword");
    //     textMeshProPwInfo = textPwInfo.GetComponent<TextMeshProUGUI>();
    //     textMeshProPwInfo.text = savedPassword;
    // }
}
