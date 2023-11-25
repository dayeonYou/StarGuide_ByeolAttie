using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    public Button stBtn;
    // Start is called before the first frame update
    void Start()
    {
        stBtn = GameObject.Find("Start").GetComponent<Button>();
    }

    public void BtnStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
