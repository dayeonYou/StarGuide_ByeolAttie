using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int Max_score = 0;
    TextMeshProUGUI score_text;

    void Start()
    {
        score_text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if( Max_score == score )
        {
            score_text.text = Max_score.ToString() + "개 중 " + score.ToString() + "개 맞았어!" + "\n" + "이제 다음 단계로 나아가보자!";
        }
        else
        {
            score_text.text = Max_score.ToString() + "개 중 " + score.ToString() + "개 맞았어!" + "\n" + "계속 도전해봐!";
        }
    }
}
