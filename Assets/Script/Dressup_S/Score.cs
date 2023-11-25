using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int Max_score = 0;
    Text score_text;
    void Start()
    {
        score_text = GetComponent<Text>();
    }

    void Update()
    {
        score_text.text = "Score : "+score.ToString()+"/"+Max_score.ToString();
    }
}
