using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Find_DetectCollision : MonoBehaviour
{
    public Find_Tracking Find_Tracking;
    public Transform gazePoint;
    public GameObject currentlyGazedObject;
    public MakeAnswer MakeAnswerScript;

    public float sec =0;

    public void Start()
    {
        Find_Tracking = GetComponent<Find_Tracking>();
    }

    public void Update()
    {
        if (Find_Tracking.isTracking)
        {
            CheckGazedObject();
        }
        if (Find_Tracking.userStatusBlink && currentlyGazedObject != null)
        {
            ExecuteEvents.Execute(currentlyGazedObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
    }

    // 현재 시선에 있는 오브젝트 찾기 
    public void CheckGazedObject()
    {
        // 시선 위치를 화면 좌표로 변환. Gazepoint 위치를 벡터로 저장 
        Vector3 gazePosition = gazePoint.position;
        // WorldToScreenPoint() 함수를 이용해서 해당 위치의 화면 좌표 얻기. 사용자의 시선 위치를 나타내는 좌표
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(gazePosition);
        // 레이를 생성하여 화면 좌표에서 화면 바라보는 방향으로 발사
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        // 레이캐스트 결과를 저장할 변수
        RaycastHit result;

        // 충돌 검출을 위한 레이캐스트
        if (Physics.Raycast(ray, out result))
        {
            GameObject hitObject = result.collider.gameObject;
            //Debug.Log("raycastresult" + hitObject.name);
            currentlyGazedObject = hitObject;

            if(hitObject.name == MakeAnswerScript.realAnswer)
            {
                sec += Time.deltaTime;
            }
        }
        else
        {
            currentlyGazedObject = null;
        }
    }
}