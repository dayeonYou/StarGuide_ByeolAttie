using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterTitle : MonoBehaviour
{
    public GameObject buttonDrawer;

    public void showBtn()
    {
        // buttonDrawer.SetActive(true);
        SceneManager.LoadScene("TwelveStars");
    }
}
