using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toAnotherScene : MonoBehaviour
{
    public string SceneName;
    public void onClickSomething()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);

    }
    
}
