using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSetting : MonoBehaviour
{
    public Image oddBar;
    public Image evenBar;
    
    public GameObject ADHDCanvas;
    public GameObject TotalCanvas;

    public Text oddText;
    public Text evenText;

    public ToggleGroup toggleGroup; // 토글 그룹
    private Dictionary<Toggle, int> toggleScores = new Dictionary<Toggle, int>(); // 토글과 점수 매핑

    public List<int> answerScores = new List<int>(); // 답변 점수를 저장할 리스트

    // Start is called before the first frame update
    void Start()
    {
        TotalCanvas.SetActive(false);
        ClearAllToggles();
        // 토글 그룹에 속한 모든 토글을 가져옵니다.
        var toggles = toggleGroup.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            var toggle = toggles[i];
            int score = i;

            // 딕셔너리에 토글-점수 쌍을 저장합니다.
            toggleScores[toggle] = score;

            // 각 토글에 이벤트 리스너를 추가합니다.
            toggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                {
                    SaveScore(QuizManager.currentQuestionIndex, score);
                    //PrintScore(toggle);
                }
            });
        }
    }

    private void SaveScore(int questionIndex, int score)
    {
        if (questionIndex >= answerScores.Count)
        {
            // 새로운 질문에 대한 답변일 경우 리스트에 추가
            answerScores.Add(score);
        }

        else
        {
            // 기존 질문에 대한 답변을 수정하는 경우 해당 위치의 값을 업데이트
            answerScores[questionIndex] = score;
        }

        UnityEngine.Debug.Log("저장된 점수: " + string.Join(", ", answerScores));

    }

    public void OnCurrentAnswer()
    {
        var toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        int previousQuestionIndex = QuizManager.currentQuestionIndex;

        if (previousQuestionIndex >= 0 && previousQuestionIndex < answerScores.Count)
        {
            int previousScore = answerScores[previousQuestionIndex];

            foreach (var toggle in toggles)
            {
                if (toggleScores[toggle] == previousScore)
                {
                    toggle.isOn = true;
                    break;
                }

                else
                {
                    toggle.isOn = false;
                }
            }
        }
        else ClearAllToggles();
    }

    // 모든 토글 체크 해제 함수
    private void ClearAllToggles()
    {
        toggleGroup.SetAllTogglesOff();
    }

    public void OnFinishButtonPressed()
    {
        int OddScore = 0;
        int EvenScore = 0;

        for(int i = 0; i< answerScores.Count; i++)
        {
            if (i % 2 == 0)
            {
                EvenScore += answerScores[i];
            }
            else
                OddScore += answerScores[i];
        }

        UnityEngine.Debug.Log("홀수 : " + OddScore);
        oddText.text = OddScore.ToString();
        if (OddScore >= 19)
        {
            oddText.color = Color.red;
        }
        UnityEngine.Debug.Log("짝수 : " + EvenScore);
        evenText.text = EvenScore.ToString();
        if (EvenScore >= 17)
        {
            evenText.color = Color.red;
        }

        UpdateBarGraph(OddScore, EvenScore);

        ADHDCanvas.SetActive(false);
        TotalCanvas.SetActive(true);

        //foreach (int score in answerScores)
        //{
        //    if
        //    totalScore += score;
        //}

        //UnityEngine.Debug.Log("총 점수: " + totalScore);
    }

    public void UpdateBarGraph(int Odd, int Even)
    {
        var oddRect = oddBar.GetComponent<RectTransform>();
        var evenRect = evenBar.GetComponent<RectTransform>();

        // Assuming the scores are a height value for the bars.
        oddRect.sizeDelta = new Vector2(oddRect.sizeDelta.x, Odd*800f);
        evenRect.sizeDelta = new Vector2(evenRect.sizeDelta.x, Even*800f);

    }
}
