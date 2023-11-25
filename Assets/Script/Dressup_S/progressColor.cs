using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressColor : MonoBehaviour
{
    public Image progressBar;
    public static int index = -1;
    public void Start()
    {
        progressBar = GameObject.Find("ProgressBar").GetComponent<Image>();
        Debug.Log("프로그래스바 확인: " + progressBar);
    }
    public void Update()
    {
        Color[] colorMappings = new Color[]
        {
            new Color(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f), //검정
            new Color(154f / 255f, 124f / 255f, 85f / 255f, 255f / 255f), //갈색
            new Color(236f / 255f, 200f / 255f, 193f / 255f, 255f / 255f), // 분홍
            new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f), //하양
            new Color(64f / 255f, 88f / 255f, 138f / 255f, 255f / 255f), //파랑
            new Color(178f / 255f, 26f / 255f, 24f / 255f, 255f / 255f), //빨강
            new Color(252f / 255f, 234f / 255f, 205f / 255f, 255f / 255f) //노랑
        };
        //Debug.Log("현재 index: 바뀌어야하는 색상: " + index);

        if (index >= 0 && index < colorMappings.Length)
        {
            progressBar.color = colorMappings[index];
            //Debug.Log("색 바꿨습니다");
        }
    }
}
