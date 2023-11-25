using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    public float minZoom = 1f; // 최소 확대 비율
    public float maxZoom = 5f; // 최대 확대 비율
    public float minX = -6f; // 최소 x 좌표
    public float maxX = 6f; // 최대 x 좌표
    public float minY = -4f; // 최소 y 좌표
    public float maxY = 4f; // 최대 y 좌표

    private Camera mainCamera;
    private float initialOrthographicSize;
    private Vector3 initialPosition;

    private float camWidth, camHeight;  // 카메라 너비/2, 높이/2
    [SerializeField] Vector2 center;    // 0, 0
    private Vector2 mapSize;   // 배경 너비, 높이
    public SpriteRenderer tf_background;    // 배경 너비, 높이를 가져오기 위한 변수

    void Start()
    {
        mainCamera = Camera.main;
        initialOrthographicSize = mainCamera.orthographicSize;
        initialPosition = mainCamera.transform.position;

        camHeight = mainCamera.orthographicSize;   // 카메라의 높이 / 2
        camWidth = camHeight * Screen.width / Screen.height;    // 카메라의 너비 / 2

        mapSize.x = tf_background.bounds.size.x;   // 배경의 너비
        mapSize.y = tf_background.bounds.size.y;	// 배경의 높이
    }

    void Update()
    {
        if (Application.isEditor)
        {
            // 에디터에서는 마우스 입력으로 카메라를 움직이기
            if (Input.GetMouseButton(0))
            {
                Vector2 touchDeltaPosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                float newX = Mathf.Clamp(mainCamera.transform.position.x - touchDeltaPosition.x * scrollSpeed * Time.deltaTime, minX, maxX);
                float newY = Mathf.Clamp(mainCamera.transform.position.y - touchDeltaPosition.y * scrollSpeed * Time.deltaTime, minY, maxY);
                mainCamera.transform.position = new Vector3(newX, newY, mainCamera.transform.position.z);

                // 카메라 범위 제한
                float dx = mapSize.x - camWidth;
                float clampX = Mathf.Clamp(mainCamera.transform.position.x, -dx + center.x, dx + center.x);

                float dy = mapSize.y - camHeight;
                float clampY = Mathf.Clamp(mainCamera.transform.position.y, -dy + center.y, dy + center.y);

                mainCamera.transform.position = new Vector3(clampX, clampY, mainCamera.transform.position.z);
            }
        }
        
        // 확대/축소 제스처 감지
        if (Input.touchCount == 2)
        {
            UnityEngine.Touch touch0 = Input.GetTouch(0);
            UnityEngine.Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 카메라 확대/축소
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + deltaMagnitudeDiff * scrollSpeed, minZoom, maxZoom);
        }

        // 스크롤 제스처 감지
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float newX = Mathf.Clamp(mainCamera.transform.position.x - touchDeltaPosition.x * scrollSpeed * Time.deltaTime, minX, maxX);
            float newY = Mathf.Clamp(mainCamera.transform.position.y - touchDeltaPosition.y * scrollSpeed * Time.deltaTime, minY, maxY);
            mainCamera.transform.position = new Vector3(newX, newY, mainCamera.transform.position.z);

            // 카메라 범위 제한
            float dx = mapSize.x - camWidth;
            float clampX = Mathf.Clamp(mainCamera.transform.position.x, -dx + center.x, dx + center.x);

            float dy = mapSize.y - camHeight;
            float clampY = Mathf.Clamp(mainCamera.transform.position.y, -dy + center.y, dy + center.y);

            mainCamera.transform.position = new Vector3(clampX, clampY, mainCamera.transform.position.z);
        }
    }

    // 확대/축소 비율 초기화
    public void ResetZoom()
    {
        mainCamera.orthographicSize = initialOrthographicSize;
        mainCamera.transform.position = initialPosition;
    }
}
