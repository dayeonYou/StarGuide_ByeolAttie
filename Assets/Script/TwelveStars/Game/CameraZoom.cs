using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 0.3f;
    public float minZoom = 1f;
    public float maxZoom = 5.5f;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            UnityEngine.Touch touchZero = Input.GetTouch(0);
            UnityEngine.Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            float newZoom = Mathf.Clamp(transform.localScale.x - difference * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
            transform.localScale = new Vector3(newZoom, newZoom, transform.localScale.z);
        }
    }
}
