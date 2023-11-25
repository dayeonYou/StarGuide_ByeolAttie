using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Mouse_EyeTrackingScript Mouse_EyeTrackingScript;
    public pause pause;

    public Rigidbody2D GazePoint2;

    public static string ClearString = "";
    public static string StartStr = "";

    public bool canMove = false;
    private bool canStopTracking = true;
    private void Start()
    {
        ClearString = "";
        StartStr = "";
    }
    private void Awake()
    {
        GazePoint2 = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && canStopTracking)
        {
            StartCoroutine(DelayedAction(1.0f)); 
            CountText.count += 1;
            UnityEngine.Debug.Log("���� �����");
            if (CountText.count == 1)
            {
                Destroy(GameObject.Find("heart1"));
                canStopTracking = true;
            }
            else if (CountText.count == 2)
            {
                Destroy(GameObject.Find("heart1 (1)"));
                canStopTracking = true;
            }
            else if (CountText.count == 3)
            {
                Destroy(GameObject.Find("heart1 (2)"));
            }

        }
        if (collision.CompareTag("Finish"))
        {
            Mouse_EyeTrackingScript.stopTracking();
            ClearString = "Clear";
            Time.timeScale = 0f;    // Ŭ�����ϸ� ȭ�� ����
            UnityEngine.Debug.Log("Ŭ����!" + ClearString);
            //gameClear.Show();
        }
        
    }
    private IEnumerator DelayedAction(float delayInSeconds)
    {
        Mouse_EyeTrackingScript.stopTracking();
        GazePoint2.gameObject.SetActive(false);
        canStopTracking = false;
        yield return new WaitForSeconds(delayInSeconds);
    }
}
