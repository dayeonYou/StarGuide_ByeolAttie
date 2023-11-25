using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timesys : MonoBehaviour
{
    private int count;
    private TextMeshProUGUI TimeText;
    public static float gTime;
    private bool isCounting = false; // �߰�: �ð� ���� ������ ���θ� ��Ÿ���� ����

    void Start()
    {
        count = 0;
        GameObject canvasObj = GameObject.Find("Canvas");
        if (canvasObj != null)
        {
            Transform timeTextTransform = canvasObj.transform.Find("Time");
            if (timeTextTransform != null)
            {
                TimeText = timeTextTransform.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("TimeText component not found. Make sure the Canvas object has a child object named 'TimeText' with a Text component.");
            }
        }
        else
        {
            Debug.LogError("Canvas object not found. Make sure a GameObject named 'Canvas' exists in the scene.");
        }
    }

    public void StartCounting() // �߰�: ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ�
    {
        if (!isCounting)
        {
            StartCoroutine(CountRoutine());
            isCounting = true;
        }
    }

    IEnumerator CountRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            count += 1;
            gTime = (float)((count / 10) + (count % 10 * 0.1));
            TimeText.text = gTime.ToString("F1"); // �Ҽ��� �Ʒ� 1�ڸ������� ǥ��
        }
    }
}