using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Text questionCount;
    
    public Toggle[] answerToggles;
    public ToggleGroup toggleGroup; // 토글 그룹
    public Image progressImage; // 프로그레스 이미지
    
    public GameObject FinishBtn; // 결과 버튼
    public GameObject NextBtn; // 다음버튼
    public static int totalScore;

    public GameObject NoAnswerPanel; // 응답없을 때 띄울 패널

    public static int currentQuestionIndex = 0;   // 질문 번호
    private List<string> questions = new List<string>() 
    { "1. 세부적인 면에 대해 꼼꼼하게 주의를 기울이지 못하거나 학업에서 부주의한 실수를 한다.", 
        "2. 손발을 가만히 두지 못하거나 의자에 앉아서도 몸을 꼼지락거린다.", 
        "3. 일을 하거나 놀이를 할 때 지속적으로 주의를 집중하는데 어려움이 있다.",
        "4. 자리에 앉아 있어야 하는 교실이나 다른 상황에서 앉아있지 못한다.",
        "5. 다른 사람이 마주보고 이야기 할 때 경청하지 않는 것처럼 보인다.",
        "6. 그렇게 하면 안 되는 상황에서 지나치게 뛰어다니거나 기어오른다.",
        "7. 지시를 따르지 않고, 일을 끝내지 못한다.",
        "8. 여가 활동이나 재미있는 일에 조용히 참여하기가 어렵다.",
        "9. 과제와 일을 체계적으로 하지 못한다.",
        "10. 끊임없이 무엇인가를 하거나 마치 모터가 돌아가듯 움직인다.",
        "11. 지속적인 노력이 요구되는 과제(학교공부나 숙제)를 하지 않으려 한다.",
        "12. 지나치게 말을 많이 한다.",
        "13. 과제나 일을 하는데 필요한 물건들을 잃어버린다.",
        "14. 질문이 채 끝나기도 전에 성급하게 답한다.",
        "15. 쉽게 산만해진다.",
        "16. 차례를 기다리는데 어려움이 있다.",
        "17. 일상적으로 하는 일을 잊어버린다.",
        "18. 다른 사람을 방해하거나 간섭한다."
    };

    void Start()
    {
        UpdateQuestionText();
        questionCount.text = "1 / " + questions.Count.ToString();
        progressImage.fillAmount = (1f / questions.Count) * 1.5f; 
        FinishBtn.SetActive(false);
        NoAnswerPanel.SetActive(false);
    }

    public void OnNextButtonPressed()
    {

        // 선택된 토글이 없는 경우 함수를 종료합니다.
        if (!toggleGroup.AnyTogglesOn())
        {
            StartCoroutine(ShowAndHideNoAnswerPanel(2));
            UnityEngine.Debug.Log("답변을 선택해주세요.");
            return;
        }
        else
        {
            currentQuestionIndex++;
            if (currentQuestionIndex >= questions.Count - 1)
            {
                UnityEngine.Debug.Log("마지막 질문입니다.");
                FinishBtn.SetActive(true);
                NextBtn.SetActive(false);
                // currentQuestionIndex = 0; // 처음으로 돌아갑니다.
            }
            UpdateQuestionText();
            StartCoroutine(FillProgress(progressImage.fillAmount + (1f / (questions.Count + 3))));
            // 코루틴을 사용하여 프로그레스 바 업데이트 실행
        }

    }

    public void OnPreviousButtonPressed()
    {
        currentQuestionIndex--;
        if (currentQuestionIndex < 0)
        {
            UnityEngine.Debug.Log("첫번째 질문입니다.");
            //currentQuestionIndex = questions.Count - 1; // 마지막으로 돌아갑니다.
        }
        else
        {
            FinishBtn.SetActive(false);
            NextBtn.SetActive(true);
            UpdateQuestionText();
        }

        StartCoroutine(FillProgress(progressImage.fillAmount - (1f / (questions.Count + 3))));
        // 코루틴을 사용하여 프로그레스 바 업데이트 실행
    }

    private void UpdateQuestionText()
    {
        questionText.text = questions[currentQuestionIndex];
        questionCount.text = (currentQuestionIndex + 1).ToString() + " / " + questions.Count.ToString();
    }

    IEnumerator FillProgress(float targetFill) // 진행도 채우는 애니메이션
    {
        float initialFill = progressImage.fillAmount;
        float timer = 0f;
        float speed = 4f;

        while (timer < 1f)
        {
            timer += Time.deltaTime * speed; // 시간을 더합니다.
            progressImage.fillAmount = Mathf.Lerp(initialFill, targetFill, timer);
            yield return null;
        }
    }

    private IEnumerator ShowAndHideNoAnswerPanel(float duration)
    {
        NoAnswerPanel.SetActive(true);

        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            // Fade out panel over time
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            var panelColor = NoAnswerPanel.GetComponent<Image>().color;
            NoAnswerPanel.GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);

            yield return null;
        }

        NoAnswerPanel.SetActive(false);
    }
}