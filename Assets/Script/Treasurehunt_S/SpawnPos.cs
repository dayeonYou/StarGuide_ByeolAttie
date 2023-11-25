using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnPos : MonoBehaviour
{
    public Transform spawnPos;
    public void ComeBack()
    {
        Debug.Log("comeBack함수로 접근되었습니다!!");
        //this.transform.Translate(new Vector3(-38, -40.7f, 0));
        Debug.Log("현재 씬은: " + SceneManager.GetActiveScene().name);
        //if (SceneManager.GetActiveScene().name == "TreasureHunt1") this.transform.position = new Vector3(-5.8f, -4.28f, 0);
        //else if (SceneManager.GetActiveScene().name == "TreasureHunt2") this.transform.position = new Vector3(5.86f, 3, 0);
        this.transform.position = spawnPos.position;
        //Time.timeScale = 0;
        Invoke("Delay", 2.0f); //2초 뒤 함수 실행. 2초 동안 시간 잠깐 멈추기 투명 벽에 부딪혀서 약간의 주의를 주는 느낌
        //Time.timeScale = 1;
    }
    void Delay()
    {
        Time.timeScale = 1;
    }
}
