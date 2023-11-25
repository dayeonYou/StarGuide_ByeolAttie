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
    public ToggleGroup toggleGroup; // ��� �׷�
    public Image progressImage; // ���α׷��� �̹���
    
    public GameObject FinishBtn; // ��� ��ư
    public GameObject NextBtn; // ������ư
    public static int totalScore;

    public GameObject NoAnswerPanel; // ������� �� ��� �г�

    public static int currentQuestionIndex = 0;   // ���� ��ȣ
    private List<string> questions = new List<string>() 
    { "1. �������� �鿡 ���� �Ĳ��ϰ� ���Ǹ� ������� ���ϰų� �о����� �������� �Ǽ��� �Ѵ�.", 
        "2. �չ��� ������ ���� ���ϰų� ���ڿ� �ɾƼ��� ���� �������Ÿ���.", 
        "3. ���� �ϰų� ���̸� �� �� ���������� ���Ǹ� �����ϴµ� ������� �ִ�.",
        "4. �ڸ��� �ɾ� �־�� �ϴ� �����̳� �ٸ� ��Ȳ���� �ɾ����� ���Ѵ�.",
        "5. �ٸ� ����� ���ֺ��� �̾߱� �� �� ��û���� �ʴ� ��ó�� ���δ�.",
        "6. �׷��� �ϸ� �� �Ǵ� ��Ȳ���� ����ġ�� �پ�ٴϰų� ��������.",
        "7. ���ø� ������ �ʰ�, ���� ������ ���Ѵ�.",
        "8. ���� Ȱ���̳� ����ִ� �Ͽ� ������ �����ϱⰡ ��ƴ�.",
        "9. ������ ���� ü�������� ���� ���Ѵ�.",
        "10. ���Ӿ��� �����ΰ��� �ϰų� ��ġ ���Ͱ� ���ư��� �����δ�.",
        "11. �������� ����� �䱸�Ǵ� ����(�б����γ� ����)�� ���� ������ �Ѵ�.",
        "12. ����ġ�� ���� ���� �Ѵ�.",
        "13. ������ ���� �ϴµ� �ʿ��� ���ǵ��� �Ҿ������.",
        "14. ������ ä �����⵵ ���� �����ϰ� ���Ѵ�.",
        "15. ���� �길������.",
        "16. ���ʸ� ��ٸ��µ� ������� �ִ�.",
        "17. �ϻ������� �ϴ� ���� �ؾ������.",
        "18. �ٸ� ����� �����ϰų� �����Ѵ�."
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

        // ���õ� ����� ���� ��� �Լ��� �����մϴ�.
        if (!toggleGroup.AnyTogglesOn())
        {
            StartCoroutine(ShowAndHideNoAnswerPanel(2));
            UnityEngine.Debug.Log("�亯�� �������ּ���.");
            return;
        }
        else
        {
            currentQuestionIndex++;
            if (currentQuestionIndex >= questions.Count - 1)
            {
                UnityEngine.Debug.Log("������ �����Դϴ�.");
                FinishBtn.SetActive(true);
                NextBtn.SetActive(false);
                // currentQuestionIndex = 0; // ó������ ���ư��ϴ�.
            }
            UpdateQuestionText();
            StartCoroutine(FillProgress(progressImage.fillAmount + (1f / (questions.Count + 3))));
            // �ڷ�ƾ�� ����Ͽ� ���α׷��� �� ������Ʈ ����
        }

    }

    public void OnPreviousButtonPressed()
    {
        currentQuestionIndex--;
        if (currentQuestionIndex < 0)
        {
            UnityEngine.Debug.Log("ù��° �����Դϴ�.");
            //currentQuestionIndex = questions.Count - 1; // ���������� ���ư��ϴ�.
        }
        else
        {
            FinishBtn.SetActive(false);
            NextBtn.SetActive(true);
            UpdateQuestionText();
        }

        StartCoroutine(FillProgress(progressImage.fillAmount - (1f / (questions.Count + 3))));
        // �ڷ�ƾ�� ����Ͽ� ���α׷��� �� ������Ʈ ����
    }

    private void UpdateQuestionText()
    {
        questionText.text = questions[currentQuestionIndex];
        questionCount.text = (currentQuestionIndex + 1).ToString() + " / " + questions.Count.ToString();
    }

    IEnumerator FillProgress(float targetFill) // ���൵ ä��� �ִϸ��̼�
    {
        float initialFill = progressImage.fillAmount;
        float timer = 0f;
        float speed = 4f;

        while (timer < 1f)
        {
            timer += Time.deltaTime * speed; // �ð��� ���մϴ�.
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