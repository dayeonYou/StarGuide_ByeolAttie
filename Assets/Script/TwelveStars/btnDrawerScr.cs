using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnDrawerScr : MonoBehaviour
{
    private bool isOn = false; // 초기 상태값은 false
    public GameObject buttonStats;
    public GameObject buttonSetting;

    public void OnButtonClick()
    {
        // 상태값을 반전시킵니다.
        isOn = !isOn;

        if (isOn)
        {
            //setting 버튼과 stats 버튼 setActive(true)
            buttonStats.SetActive(true);
            buttonSetting.SetActive(true);
            //setting 버튼과 stats 버튼 나오는 애니메이션
            Animator animatorStats = buttonStats.GetComponent<Animator>();
            Animator animatorSetting = buttonSetting.GetComponent<Animator>();

            animatorStats.Play("moveBtnStats");
            animatorSetting.Play("moveBtnSetting");
        }
        else
        {
            //setting 버튼과 stats 버튼 들어가는 애니메이션
            Animator animatorStats = buttonStats.GetComponent<Animator>();
            Animator animatorSetting = buttonSetting.GetComponent<Animator>();

            animatorStats.Play("moveBtnStats2");
            animatorSetting.Play("moveBtnSetting2");
            //setting 버튼과 stats 버튼 setActive(false)
        }
    }
    public void OnAnimationEnd(){
        buttonStats.SetActive(false);
        buttonSetting.SetActive(false);
    }

}
