using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCount : MonoBehaviour
{
    private TextMeshProUGUI text;
    public static int count;

    [SerializeField]
    public int bullet;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        count = bullet;
        if (text == null)
        {
            Debug.LogError("CountText script requires a Text component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            text.text = count.ToString() + "/" + bullet ;
        }
    }
}
