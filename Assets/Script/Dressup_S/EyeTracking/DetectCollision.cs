using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DetectCollision : MonoBehaviour
{
    public EyeTrakingScript EyeTrakingScript;
    public Transform gazePoint;
    public GameObject currentlyGazedObject;

    public static bool Touch = false;
    //public progressColor Changecolor;
    /* gazeTimer */
    public Image ProgressBar;
    float currentValue;
    float speed;
    private bool isGazing;
    public float gazeDuration;
    private float gazeTimer;

    //private GameObject GazeParent; //ProgressBar의 부모인 Gazepoint도 비활성화 상태이므로 Awake()함수에서 부모를 구해놓기
    /*
    public void Awake()
    {
        //GazeParent = GameObject.Find("Canvas").transform.Find("Gazepoint").gameObject;
        //ProgressBar = GameObject.Find("Gazepoint").transform.Find("ProgressBar").GetComponent<Image>();
    }
    */
    public void Start()
    {
        //Changecolor = GameObject.Find("ProgressBar").GetComponent<progressColor>();
        EyeTrakingScript = GetComponent<EyeTrakingScript>();
        speed = 100 / gazeDuration;
    }

    public void Update()
    {
        if (EyeTrakingScript.isTracking)
        {
            CheckGazedObject();

            /* gazeTimer */ 
            if (currentlyGazedObject != null)
            {
                if (isGazing)
                { 
                    gazeTimer += Time.deltaTime;
                    //Debug.Log("gazeTimer: " + gazeTimer);
                    //Debug.Log("currenrtlyGazeObject: " + currentlyGazedObject);

                    //프로그래스바 색 바꾸는 함수. 현재 아이트래킹으로 충돌된 오브젝트의 이름을 전달인자로 함.
                    //Changecolor.ChangeColor_Bar(currentlyGazedObject.name);
                    
                    switch (currentlyGazedObject.name)
                    {
                        case "Button_hat":
                            progressColor.index = RandomCloth.hat_F;
                            Debug.Log("모자 색상: 아이트래킹용" + progressColor.index);
                            break;
                        case "Button_shirt":
                            progressColor.index = RandomCloth.shirt_F;
                            Debug.Log("상의 색상: 아이트래킹용" + progressColor.index);
                            break;
                        case "Button_pant":
                            progressColor.index = RandomCloth.pant_F;
                            Debug.Log("하의 색상: 아이트래킹용" + progressColor.index);
                            break;
                        case "Button_shoes":
                            progressColor.index = RandomCloth.shoes_F;
                            Debug.Log("신발 색상: 아이트래킹용" + progressColor.index);
                            break;
                        default:
                            Debug.Log("progressBar 색을 바꿀 수 없습니다!!");
                            break;
                    }
                    
                    if (currentValue < 100)
                    {
                        currentValue += speed * Time.deltaTime;
                    }

                    ProgressBar.fillAmount = currentValue / 100;

                    if (gazeTimer >= gazeDuration)
                    {
                        Debug.Log("Click!");
                        Debug.Log("gazeTimer: " + gazeTimer);
                        Touch = true;
                        ExecuteEvents.Execute(currentlyGazedObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        gazeTimer = 0f;
                        currentValue = 0;
                        ProgressBar.fillAmount = 0;
                    }
                    Touch = false;
                }
                else
                {
                    isGazing = true;
                    gazeTimer = 0f;
                    currentValue = 0;
                    ProgressBar.fillAmount = 0;
                    Touch = false;
                }
            }
            else
            {
                isGazing = false;
                currentValue = 0;
                ProgressBar.fillAmount = 0;
                Touch = false;
            }
            
            if (NotShow_Cloth.click_hat > 0 && NotShow_Cloth.click_shirt > 0 && NotShow_Cloth.click_pant > 0 && NotShow_Cloth.click_shoes > 0)
            {
                Debug.Log("아이템 전체가 다 가려졌습니다!!");
                GameObject.Find("Canvas").transform.Find("Panel_closet").gameObject.SetActive(true);

                EyeTrakingScript.stopTracking();

            }
        }
    }

    //현재 시선에 있는 오브젝트 찾기 
    public void CheckGazedObject()
    {
        // 시선 위치를 화면 좌표로 변환. Gazepoint 위치를 벡터로 저장 
        Vector3 gazePosition = gazePoint.position;
        // WorldToScreenPoint() 함수를 이용해서 해당 위치의 화면 좌표 얻기. 사용자의 시선 위치를 나타내는 좌표
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(gazePosition);
        // EventSystem.current를 전달하여 현재 활성화된 이벤트 시스템을 사용하도록 설정
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        // 현재 시선 위치를 이벤트 위치로 설정.
        pointerEventData.position = screenPosition;

        //레이 케스트 결과를 저장할 빈 리스트 생성 
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //전달된 포인터이벤트에 따라 모든 레이캐스트 결과 리스트에 저장 
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        //교차되는 UI 요소의 게임 오브젝트를 저장할 변수 
        GameObject newGazedObject = null;

        // Gazepoint 오브젝트의 새로운 Layer
        int gazepointLayer = LayerMask.NameToLayer("Gazepoint");
        int ButtonLayer = LayerMask.NameToLayer("Button");

        foreach (RaycastResult result in raycastResults) //RaycastResult result in raycastResults
        {
            // Gazepoint 오브젝트를 무시하고 다음 요소로 이동
            if (result.gameObject.layer == gazepointLayer)
            {
                continue;
            }
            // Button 오브젝트만 판별
            if (result.gameObject.layer == ButtonLayer)
            {
                //교차되는 UI 요소의 게임 오브젝트 이름 반환 
                Collider2D collider2D = result.gameObject.GetComponent<Collider2D>();
                Debug.Log("interaction: " + result.gameObject.name);

                newGazedObject = result.gameObject;
                currentlyGazedObject = newGazedObject;
            }
        }

        if (newGazedObject != currentlyGazedObject)
        {
            currentlyGazedObject = newGazedObject;
        }

    }
}


