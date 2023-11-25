using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float minX = -5f; // 최소 x 좌표
    public float maxX = 5f; // 최대 x 좌표
    public float minY = -5f; // 최소 y 좌표
    public float maxY = 5f; // 최대 y 좌표

    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float newX = Mathf.Clamp(transform.position.x - touchDeltaPosition.x * scrollSpeed * Time.deltaTime, minX, maxX);
            float newY = Mathf.Clamp(transform.position.y - touchDeltaPosition.y * scrollSpeed * Time.deltaTime, minY, maxY);
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }
}
