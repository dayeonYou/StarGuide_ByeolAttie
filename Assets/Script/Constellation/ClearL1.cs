using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearL1 : MonoBehaviour
{
    public GameObject L_O;
    public string animationName;
    private bool isClickable = true; // 클릭 가능한 상태인지 여부
    public string SceneName;
    public GameObject WindowObjects;
    public GameObject[] NotShowOb;

    // 클릭 이벤트를 처리할 함수
    private void OnMouseDown()
    {
        if (isClickable)
        {
            // 클릭 가능한 상태일 때 처리할 내용을 여기에 작성
            Debug.Log("Object Clicked!");
            // Animator animator = L_O.GetComponent<Animator>();
            // animator.Play(animationName);

            // SceneManager.LoadScene(SceneName);
            // LoadingSceneManager.LoadScene(SceneName);
            
            WindowObjects.SetActive(true);
            for(int i=0;i<NotShowOb.Length;i++){
                NotShowOb[i].SetActive(false);
            }
            
            
            Debug.Log("클릭");
        }
        else
        {
            // 클릭 불가능한 상태일 때 처리할 내용을 여기에 작성
            Debug.Log("Object is not clickable.");
        }
    }

    // 클릭 가능한 상태 설정 함수
    public void SetClickable(bool clickable)
    {
        isClickable = clickable;
    }
}
