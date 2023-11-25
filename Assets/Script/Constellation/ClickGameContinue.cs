using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickGameContinue : MonoBehaviour
{
    public string SceneName;

    public void clickGameContinue(){
        LoadingSceneManager.LoadScene(SceneName);
    }

}
