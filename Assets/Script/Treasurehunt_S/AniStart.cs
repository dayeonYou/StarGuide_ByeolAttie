using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniStart : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim.SetBool("ButtonPush", false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭한 좌표값 가져오기
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.name == "startButton")
                {
                    buttonPush();
                }
            }
        }
    }
    public void buttonPush()
    {
        anim.SetBool("ButtonPush", true);
    }
}
