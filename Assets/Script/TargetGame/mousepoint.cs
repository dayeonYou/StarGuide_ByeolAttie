using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousepoint : MonoBehaviour
{

    private Rigidbody2D rb;
    public static string OverString = "";

    // Start is called before the first frame update
    private void Start()
    {
        OverString = "";
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 

        // 오브젝트를 마우스 위치로 이동
        rb.MovePosition(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Target"))
            {
                BulletCount.count--;
                // 클릭한 게 타겟인 경우
                Debug.Log("타겟 클릭!");
            }
            else
            {

            }
        }
    }
}
