using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimited : MonoBehaviour
{
    private Text TimeText;
    public static float rTime = 120;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        TimeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        rTime -= Time.deltaTime;

        if (rTime < 0) rTime = 0;
        TimeText.text = "남은 시간 : " + Mathf.Round(rTime);
    }
}
