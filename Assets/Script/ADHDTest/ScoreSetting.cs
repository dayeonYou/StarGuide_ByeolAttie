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

    public ToggleGroup toggleGroup; // ��� �׷�
    private Dictionary<Toggle, int> toggleScores = new Dictionary<Toggle, int>(); // ��۰� ���� ����

    public List<int> answerScores = new List<int>(); // �亯 ������ ������ ����Ʈ

    // Start is called before the first frame update
    void Start()
    {
        TotalCanvas.SetActive(false);
        ClearAllToggles();
        // ��� �׷쿡 ���� ��� ����� �����ɴϴ�.
        var toggles = toggleGroup.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            var toggle = toggles[i];
            int score = i;

            // ��ųʸ��� ���-���� ���� �����մϴ�.
            toggleScores[toggle] = score;

            // �� ��ۿ� �̺�Ʈ �����ʸ� �߰��մϴ�.
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
            // ���ο� ������ ���� �亯�� ��� ����Ʈ�� �߰�
            answerScores.Add(score);
        }

        else
        {
            // ���� ������ ���� �亯�� �����ϴ� ��� �ش� ��ġ�� ���� ������Ʈ
            answerScores[questionIndex] = score;
        }

        UnityEngine.Debug.Log("����� ����: " + string.Join(", ", answerScores));

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

    // ��� ��� üũ ���� �Լ�
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

        UnityEngine.Debug.Log("Ȧ�� : " + OddScore);
        oddText.text = OddScore.ToString();
        if (OddScore >= 19)
        {
            oddText.color = Color.red;
        }
        UnityEngine.Debug.Log("¦�� : " + EvenScore);
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

        //UnityEngine.Debug.Log("�� ����: " + totalScore);
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
