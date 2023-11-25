using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassWall : MonoBehaviour
{
    //public SpawnPos position;
    public GameObject obj;
    public void OnTriggerEnter2D(Collider2D other)
    {
        //player = GameObject.Find("Canvas").transform.Find("collegestudent1").gameObject;//null값 안 들어가게 해줌! 변수 선언 오류 해결!!
        //player = GameObject.Find("collegestudent");//null값 안 들어가게 해줌! 변수 선언 오류 해결!!
        //position = GameObject.Find("Canvas").transform.Find("spawnPos").gameObject.GetComponent<SpawnPos>();
        obj = GameObject.Find("Canvas").transform.Find("spawnPos").gameObject;
        Debug.Log(obj);
        if (other.gameObject.tag == "Player") //주인공에게 직접 부딪혔을때
        {
            Debug.Log("투명벽이 있습니다. 정해진 경로로 이동하세요.");
            //position.ComeBack();
            obj.GetComponent<SpawnPos>().ComeBack();
        }
    }
}
