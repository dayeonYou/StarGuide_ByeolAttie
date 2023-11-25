using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    public GameObject rangeOb; // ���� ������Ʈ �ޱ�
    CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = rangeOb.GetComponent<CircleCollider2D>();
    }

    private void OnMouseDown()
    {
        Vector2 originpo = rangeOb.transform.position;
        Debug.Log("Ŭ���� ������Ʈ : " + gameObject.name);
        if (BulletCount.count > 0)
        {
            Destroy(gameObject);
            TargetCount.count++;
        }
    }
}