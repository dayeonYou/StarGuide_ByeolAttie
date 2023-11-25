using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Target_GazeInteraction : MonoBehaviour
{
    public Target_EyeTrackingScript Target_EyeTrackingScript;
    public Transform gazePoint;
    private GameObject currentlyGazedObject;
    public static string OverString = "";
    public static bool Touch = false;

    /* blink */
    // private bool hasBlinked = true;


    /* gazeTimer */
    public Image LoadingBar;
    float currentValue;
    float speed; // speed * gazeDuration = 100
    private bool isGazing;
    public float gazeDuration;
    private float gazeTimer;
    public float Timer;

    private void Start()
    {
        OverString = "";
        Target_EyeTrackingScript = GetComponent<Target_EyeTrackingScript>();
        speed = 100 / gazeDuration;
    }

    private void Update()
    {
        if (Target_EyeTrackingScript.isTracking)
        {
            CheckGazedObject();
            Touch = false;

            /* blink */
            /*
            if (Target_EyeTrackingScript.userStatusBlink && !hasBlinked)
            {
                hasBlinked = true;
                if (BulletCount.count > 0)
                {
                    BulletCount.count--;
                    Debug.Log("BulletCount: " + BulletCount.count);
                }
                else if (BulletCount.count == 0)
                {
                    Time.timeScale = 0f;
                    OverString = "GameOver";
                    UnityEngine.Debug.Log("Game Over!" + OverString);
                }
                if (currentlyGazedObject != null)
                {
                    Touch = true;
                    ExecuteEvents.Execute(currentlyGazedObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                }
                Debug.Log("Click!");
            }
            else if (!Target_EyeTrackingScript.userStatusBlink)
            {
                hasBlinked = false;
                Touch = false;
            }
            */


            /* gazeTimer */
            if (currentlyGazedObject != null)
            {
                if (isGazing)
                {
                    gazeTimer += Time.deltaTime;
                    Debug.Log("gazeTimer: " + gazeTimer);

                    if (currentValue < 100)
                    {
                        currentValue += speed * Time.deltaTime;
                    }

                    LoadingBar.fillAmount = currentValue / 100;

                    if (gazeTimer >= gazeDuration)
                    {
                        /*
                        if (BulletCount.count > 0)
                        {
                            BulletCount.count--;
                            Debug.Log("BulletCount: " + BulletCount.count);
                        }
                        else if (BulletCount.count == 0)
                        {
                            Time.timeScale = 0f;
                            OverString = "GameOver";
                            UnityEngine.Debug.Log("Game Over!" + OverString);
                        }
                        */
                        Debug.Log("Click!");
                        Touch = true;
                        ExecuteEvents.Execute(currentlyGazedObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        Timer += gazeTimer;
                        gazeTimer = 0f;
                        currentValue = 0;
                        LoadingBar.fillAmount = 0;
                    }
                    Touch = false;
                }
                else
                {
                    isGazing = true;
                    Timer += gazeTimer;
                    gazeTimer = 0f;
                    currentValue = 0;
                    LoadingBar.fillAmount = 0;
                    Touch = false;
                }
            }
            else
            {
                isGazing = false;
                currentValue = 0;
                LoadingBar.fillAmount = 0;
                Touch = false;
            }
            
        }
    }

    private void CheckGazedObject()
    {
        Vector3 gazePosition = gazePoint.position;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(gazePosition);
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = screenPosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        GameObject newGazedObject = null;
        foreach (RaycastResult result in raycastResults)
        {
            Collider2D collider2D = result.gameObject.GetComponent<Collider2D>();

            Debug.Log("interaction: " + result.gameObject.name);

            if (collider2D != null)
            {
                newGazedObject = result.gameObject;
                break;
            }
        }

        if (newGazedObject != currentlyGazedObject)
        {
            if (currentlyGazedObject != null)
            {
                // 포커스를 잃을 때 처리
                // 오트젝트에는 포커스가 없는 듯..
            }

            if (newGazedObject != null)
            {
                // 포커스가 생길 때 처리
            }

            currentlyGazedObject = newGazedObject;
        }
    }
}
