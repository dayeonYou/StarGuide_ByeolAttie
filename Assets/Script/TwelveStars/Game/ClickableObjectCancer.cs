using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableObjectCancer : MonoBehaviour
{
    public string ConstellationSceneName;

    // 클릭 이벤트를 처리할 함수
    private void OnMouseDown()
    {
        // SceneManager.LoadScene(ConstellationSceneName);
        LoadingSceneManager.LoadScene(ConstellationSceneName);
        Debug.Log("클릭");
    }
}