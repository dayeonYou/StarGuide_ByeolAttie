using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSetActive : MonoBehaviour
{
    public GameObject buttonReward;
    public GameObject buttonMyPage;

    public void OnAnimationEnd(){
        buttonReward.SetActive(false);
        buttonMyPage.SetActive(false);
    }

}