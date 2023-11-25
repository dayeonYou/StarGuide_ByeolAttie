using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mouse_DetectCollision : MonoBehaviour
{
    public Mouse_EyeTrackingScript Mouse_EyeTrackingScript;
    public pause pause;

    public Transform gazePoint;
    public GameObject currentlyGazedObject;

    public void Start()
    {
        Mouse_EyeTrackingScript = GetComponent<Mouse_EyeTrackingScript>();
    }

    public void Update()
    {
        if (Mouse_EyeTrackingScript.isTracking)
        {
            CheckGazedObject();
        }
        if (Mouse_EyeTrackingScript.userStatusBlink && currentlyGazedObject != null)
        {
            ExecuteEvents.Execute(currentlyGazedObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
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

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.layer == gazepointLayer)
            {
                continue; // Gazepoint 오브젝트를 무시하고 다음 요소로 이동
            }

            //교차되는 UI 요소의 게임 오브젝트 이름 반환 
            Collider2D collider2D = result.gameObject.GetComponent<Collider2D>();
            //Debug.Log("interaction: " + result.gameObject.name);

            if (collider2D != null)
            {
                newGazedObject = result.gameObject;
                //Debug.Log("raycastResults: " + result.gameObject.name);
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
