using UnityEngine;
using TMPro;
using DG.Tweening;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;


public class ObjectMovement : MonoBehaviour
{
    public float orbitRadius = 3.5f; // 궤도 반지름
    public float rotationSpeed = 30f; // 이동 속도 (각도 단위)
    public Transform[] objectsToMove; // 함께 움직일 오브젝트들
    public float[] currentAngle;
    public int numOfIndex;

    public string nameOfConstellation;
    public string dateOfConstellation;

    private Vector2 centerPosition; // 궤도 중심 위치

    private bool isRotating = false; // 회전 중인지 여부

    public GameObject text1; // 텍스트프로메쉬 텍스트 오브젝트
    public GameObject text2;
    TextMeshProUGUI textMeshPro1;
    TextMeshProUGUI textMeshPro2;
    
    public AudioClip backgroundMusicClip;
    private AudioSource audioSource;

    DatabaseReference databaseReference;
    private bool isClear = false;

    private void Start()
    {
        //
        transform.position = new Vector2(transform.position.x, transform.position.y);

        // 궤도 중심 위치 설정
        centerPosition = Vector2.zero;

        textMeshPro1 = text1.GetComponent<TextMeshProUGUI>();
        textMeshPro2 = text2.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            // 모든 오브젝트의 AudioSource 찾기
            AudioSource[] allAudioSources = GameObject.FindObjectsOfType<AudioSource>();

            foreach (AudioSource otherAudioSource in allAudioSources)
            {
                // 현재 재생 중인 AudioSource 멈추기
                if (otherAudioSource.isPlaying)
                {
                    otherAudioSource.Stop();
                }
            }
            //
            for (int k = 0; k < numOfIndex; k++)
                currentAngle[k] = GetAngleFromPosition(objectsToMove[k].position);

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject) //2d collider 컴포넌트 추가
            {
                // Initialize Firebase
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
                {
                    FirebaseApp app = FirebaseApp.DefaultInstance;
                    databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                    LoadAndDeactivateReward();

                });
                for (int i = 0; i < numOfIndex; i++)
                {
                    float currentAngleClicked = GetAngleFromPosition(transform.position);
                    float requiredAngle = 90f;
                    float angleDifference = requiredAngle - currentAngleClicked;
                    if (angleDifference < -5)
                        angleDifference += 360f;

                    // Round the angle to the nearest multiple of 30
                    int roundedAngle = Mathf.RoundToInt(angleDifference / 30f) * 30;
                    rotationSpeed = roundedAngle;
                    StartCoroutine(RotateObject(i));
                }
                Debug.Log("Required Angle: " + rotationSpeed + " degrees");
                Debug.Log("hit: " + hit.collider.gameObject);

                StartCoroutine(ChangeTextWithAnimation());
            }
        }
    }

    float returnLastPosition(float targetNumber){
        // 가장 가까운 숫자를 찾기 위한 변수 초기화
        float closestNumber = 0f;
        float closestDistance = float.MaxValue;

        // 비교할 숫자들의 배열
        float[] numbersToCompare = { -3.03f, 1.75f, -1.75f, 3.03f, 0.00f, -3.5f, 3.5f };

        // 각 숫자들과 대상 숫자(targetNumber)의 거리를 비교하여 가장 가까운 숫자 찾기
        foreach (float num in numbersToCompare)
        {
            float distance = Mathf.Abs(num - targetNumber);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNumber = num;
            }
        }
        return closestNumber;
    }

    private System.Collections.IEnumerator RotateObject(int i)
    {
        isRotating = true;

        float targetAngle = currentAngle[i] + rotationSpeed;
        int roundedAngle = Mathf.RoundToInt(targetAngle / 30f) * 30;
        targetAngle = roundedAngle;

        while ((currentAngle[i] <= targetAngle))
        {
            // 다음 각도 계산
            float angleInRadians = currentAngle[i] * Mathf.Deg2Rad;
            
            float x = centerPosition.x + orbitRadius * Mathf.Cos(angleInRadians);
            float y = centerPosition.y + orbitRadius * Mathf.Sin(angleInRadians);
            // 오브젝트 이동
            objectsToMove[i].position = new Vector2(x, y);
            
            // 각도 업데이트
            currentAngle[i] += Time.deltaTime * rotationSpeed;
            
            // rotation 값 증가
            objectsToMove[i].Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            if((currentAngle[i] >= targetAngle)){
                x= returnLastPosition(x);
                y= returnLastPosition(y);
                // Debug.Log("가장 가까운 숫자x: " + x);
                objectsToMove[i].position = new Vector2(x, y);
                
                float currentZRotation = objectsToMove[i].transform.eulerAngles.z;
                currentZRotation = Mathf.RoundToInt(currentZRotation / 30f) * 30;
                objectsToMove[i].rotation = Quaternion.Euler(0f, 0f, currentZRotation); // z축 회전 값을 currentZRotation으로 설정
                if(isClear){
                    Debug.Log("clear~~");
                    PlayRewardMusic();
                    isClear = false;
                }
                break;
            }
            yield return null;
        }
        
        // Debug.Log(currentAngle[i]+"currnetAngle");
        // Debug.Log(targetAngle+"targetAngle");

        isRotating = false;
    }


    // 주어진 위치로부터 원의 중심까지의 각도를 계산하는 함수
    private float GetAngleFromPosition(Vector2 position)
    {
        Vector2 direction = position - centerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0f)
        {
            angle += 360f;
        }
        return angle;
    }
    private System.Collections.IEnumerator ChangeTextWithAnimation()
    {
        // 천천히 사라지는 애니메이션
        float duration = 1.0f;
        textMeshPro1.DOFade(0f, duration);
        textMeshPro2.DOFade(0f, duration);
        yield return new WaitForSeconds(duration);

        // 텍스트 변경
        textMeshPro1.text = nameOfConstellation;
        textMeshPro2.text = dateOfConstellation;

        // 천천히 나타나는 애니메이션
        textMeshPro1.DOFade(1f, duration);
        textMeshPro2.DOFade(1f, duration);
    }

    private void PlayRewardMusic()
    {
        // 모든 오브젝트의 AudioSource 찾기
        AudioSource[] allAudioSources = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource otherAudioSource in allAudioSources)
        {
            // 현재 재생 중인 AudioSource 멈추기
            if (otherAudioSource.isPlaying)
            {
                otherAudioSource.Stop();
            }
        }
        
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusicClip;

        // 저장된 배경음 크기 로드
        float volume = PlayerPrefs.GetFloat("SliderValueB", 1f);
        Debug.Log("backgroundMusic Value: "+volume);
        // 배경음 크기 설정
        audioSource.volume = volume;
        // 배경음 재생
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("backgroundMusic : ok");
        
        }
    }
    private void LoadAndDeactivateReward()
    {
        string CurrentUserID = "";
        CurrentUserID = PlayerPrefs.GetString("UserID");
        // CurrentUserID = bQMkmHlOM6VYYVYv5VCXnBXQSh13
        string objectName = gameObject.name;
        Debug.Log(CurrentUserID);
        databaseReference.Child("User+"+CurrentUserID).Child("Constellation").Child(objectName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    int count = int.Parse(snapshot.Value.ToString());
                    if(count == -1){
                        isClear = true;
                        Debug.Log(objectName + "clear!" + isClear);
                    }
                    else Debug.Log(objectName + "notClear!" + isClear);

                }
                else{
                    Debug.Log("notFound!+"+CurrentUserID+"objectname:"+objectName);
                }
                
            }
            else{
                Debug.Log("notYet!");
            }
        });
    }
}
