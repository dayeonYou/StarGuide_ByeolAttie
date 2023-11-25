using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public GameObject gameClearPanel;
    bool isGameClear = false;

    void Start()
    {
        gameClearPanel = GameObject.Find("GameClear");
        if (gameClearPanel)
        {
            gameClearPanel.SetActive(false);
        }
        else
            Debug.Log("No gameClearPanel called panel found");
        UnityEngine.Debug.Log("Ŭ����! ��Ȱ��ȭ");
    }

    void Update()
    {
        if (Movement.ClearString == "Clear" || TargetCount.ClearString == "Clear")
        {
            UnityEngine.Debug.Log("Update Ŭ����! Ȯ��" + Movement.ClearString);
            isGameClear = true;
            if(isGameClear == true) Show();
            UnityEngine.Debug.Log("Ŭ����! Ȱ��ȭ");
        }

    }

    private void Show()
    {
            gameClearPanel.SetActive(true);
            
    }
}