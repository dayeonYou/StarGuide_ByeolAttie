using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn2 : MonoBehaviour
{
    public pause pause;
    public Movement movement;
    public Mouse_EyeTrackingScript Mouse_EyeTrackingScript;

    public GameObject GazePoint2; // �÷��̾� �������� �Ҵ��� ����
    public Transform spawnPoint; // ���� ��ġ�� �Ҵ��� ����

    private float scaleFactor;
    public int maxCount = 3;
    public static string OverString = "";

    private void Start()
    {
        OverString = "";
        if (!pause) Debug.LogError("Pause script is not assigned");
    }
    private void Update()
    {
        if (pause.Startstate && !Mouse_EyeTrackingScript.isTracking)
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        GazePoint2 = Mouse_EyeTrackingScript.GazePoint2;
        
        if (!Mouse_EyeTrackingScript.isTracking && spawnPoint !=null)
        {
            if (CountText.count < maxCount) // 3���� ����
            {
                pause.StartPanel.SetActive(true); // �ε����� �� �ٽ� ���� ����
                pause.Startstate = false;
                IncreasePlayerSize();
                Debug.Log("ũ�Ⱑ Ŀ��");
                Debug.Log("�ε��� Ƚ�� : " + CountText.count);
            }
            else
            {
                Time.timeScale = 0f;
                OverString = "GameOver";
                UnityEngine.Debug.Log("Game Over!" + OverString);
                Debug.Log("�ε��� Ƚ�� : " + CountText.count + "-���ӿ��� ����");
            };
        }
        else
        {
            Debug.LogError("GameManager is missing playerPrefab or spawnPoint references.");
        }
        
    } 
    
    private void IncreasePlayerSize()
    {
        scaleFactor += (CountText.count * 0.025f);
        Vector3 newSize = GazePoint2.transform.localScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
        GazePoint2.transform.localScale = newSize;
    }
}


