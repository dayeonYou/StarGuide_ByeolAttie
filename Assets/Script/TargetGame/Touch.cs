using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    public GameObject rangeOb; // 하위 오브젝트 받기
    CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = rangeOb.GetComponent<CircleCollider2D>();
    }

    private void OnMouseDown()
    {
        Vector2 originpo = rangeOb.transform.position;
        Debug.Log("클릭된 오브젝트 : " + gameObject.name);
        if (BulletCount.count > 0)
        {
            Destroy(gameObject);
            TargetCount.count++;
        }
    }
}