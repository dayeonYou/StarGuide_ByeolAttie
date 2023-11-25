using UnityEngine;
using System.Collections;

public class ColliderController : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();


        if ((boxCollider != null) && Input.GetMouseButtonDown(0))
        {
            boxCollider.enabled = false;

            // 1초 후에 ActivateCollider 코루틴을 시작
            StartCoroutine(ActivateColliderAfterDelay(1f));
        }
        else
        {
            Debug.LogError("BoxCollider2D not found on this GameObject!");
        }
    }

    IEnumerator ActivateColliderAfterDelay(float delay)
    {
        // delay 시간 동안 대기
        yield return new WaitForSeconds(delay);

        // BoxCollider2D를 활성화
        boxCollider.enabled = true;
    }
}
