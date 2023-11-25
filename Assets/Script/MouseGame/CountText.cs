using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountText : MonoBehaviour
{
    private Text text;
    public static int count = 0;

    void Awake()
    {
        text = GetComponent<Text>();
        if (text == null)
        {
            Debug.LogError("CountText script requires a Text component.");
        }
    }

    void Update()
    {
        if (text != null)
        {
            //text.text = "COUNT : " + count.ToString();
            UnityEngine.Debug.Log("�ε��� Ƚ�� : " + count);
        }
    }
}
