using UnityEngine;
using TMPro;
using System;

public class DateBtn : MonoBehaviour
{
    public TextMeshProUGUI dateText;

    public void Start()
    {
        // 현재 날짜를 가져와서 텍스트로 변환
        string currentDate = DateTime.Now.ToString("yyyy/MM/dd");

        // 텍스트 업데이트
        dateText.text = currentDate;
    }
}
