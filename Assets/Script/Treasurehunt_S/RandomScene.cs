using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomScene : MonoBehaviour
{
    private int sceneNum;

    public void Scene()
    {
        sceneNum = Random.Range(1, 3); //1~2의 정수
        Debug.Log("랜덤 씬 " + sceneNum + "번 입니다.");
        SceneManager.LoadScene("TreasureHunt"+sceneNum);
        Time.timeScale = 1;
    }
}
