using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableObject : MonoBehaviour
{
    public string ConstellationSceneName;
    private bool isPressed = false;
    private float pressStartTime;

    // 클릭 이벤트를 처리할 함수
    private void OnMouseDown()
    {
        isPressed = true;
        pressStartTime = Time.time;
    }

    // 뗐을 때의 처리 함수
    private void OnMouseUp()
    {
        isPressed = false;
        
        // Check if the press duration is greater than or equal to 0.3 seconds
        if (Time.time - pressStartTime >= 0.3f)
        {
            // SceneManager.LoadScene(ConstellationSceneName);
            LoadingSceneManager.LoadScene(ConstellationSceneName);
            Debug.Log("클릭");
        }
    }
}

// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class ClickableObject : MonoBehaviour
// {
//     public string ConstellationSceneName;

//     // 클릭 이벤트를 처리할 함수
//     private void OnMouseDown()
//     {
//         // SceneManager.LoadScene(ConstellationSceneName);
//         LoadingSceneManager.LoadScene(ConstellationSceneName);
//         Debug.Log("클릭");
//     }
// }