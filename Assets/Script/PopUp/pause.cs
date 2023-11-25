using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public GameObject gamePausedPanel; // ���� Ŭ���� �г� ������Ʈ�� �Ҵ�
    public GameObject StartPanel;

    public Button stBtn;
    public Button psBtn;

    public bool Startstate = false;

    void Start()
    {
        Startstate = true;
        gamePausedPanel.SetActive(false);
        psBtn.interactable = false; // 처음 중지버튼 비활성화
    }

    public void Show()
    {
        if (Movement.ClearString == "Clear")
        {
            Startstate = true;
            gamePausedPanel.SetActive(false);
        }
        else
        {
            Startstate = false;
            gamePausedPanel.SetActive(true);
            Time.timeScale = 0; //���� �Ͻ�����
        }
    }

    public void Continue()
    {
        Startstate = true;
        Time.timeScale = 1;
        gamePausedPanel.SetActive(false);
    }

    public void OnClick_Retry() // '�絵��' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        Time.timeScale = 1;

        CountText.count = 0;
        TimeLimited.rTime = 120;

        Timesys.gTime = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // SceneManager�� LoadScene �Լ��� ����Ͽ�! ���� �� 'GameScene'�� �ٽ� �ҷ������� ��Ų��.
                                                                    // ���� ���� �ٽ� �ҷ����� ������ ����� �ȴ�.
        psBtn.interactable = false;
        stBtn.interactable = true;
    }

    public void onClickStartButton() // 마우스게임 레디패널
    {
        Startstate = true;
        Time.timeScale = 1;
        StartPanel.SetActive(false);
        //Movement123.StartStr = "true";
    }

    public void StartButtonState()
    {
        stBtn.interactable = false;
        psBtn.interactable = true;
    }
}