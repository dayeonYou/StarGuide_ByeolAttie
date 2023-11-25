using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShuffleCups : MonoBehaviour
{

    GenerateCup generateCupScript;
    ObjectSelector objectSelector;
    MakeAnswer makeAnswerScript;
    
    private Coroutine shuffleCoroutine;

    public int shuffleCount = 10; // 섞는 횟수
    public float shuffleDuration = 10f; // 섞이는 시간 초
    public GameObject[] cups;
    public Vector3[] positions;
    private int currentPositionIndex = 0;
    private bool isShuffling = false;

    public Button stBtn;

    public Sprite disabledImage;

    public int maxClicks = 30; // 최대 클릭 횟수

    public int clickCount = 0; // 현재 클릭 횟수

    public GameObject squareWindowPause;
    public Button pauseBtn;
    bool shuffleOver = false;
    
    public GameObject WrongAnswerImage;
    public GameObject CorrectAnswerImage;

    void Start()
    {
        stBtn = GameObject.Find("stBtn").GetComponent<Button>();

        makeAnswerScript = FindObjectOfType<MakeAnswer>();
        Cupinit();
    }
   public IEnumerator Shuffle()
    {

        isShuffling = true;
        for (int count = 0; count < shuffleCount; count++)
        {
            // 컵을 무작위로 섞기 전에, 이동할 위치를 미리 계산합니다.
            Vector3[] targetPositions = new Vector3[cups.Length];
            Vector3[] positionsCopy = (Vector3[])positions.Clone(); // positions 배열 복사
            for (int i = 0; i < cups.Length; i++)
            {
                int randomIndex = Random.Range(i, cups.Length);
                targetPositions[i] = positionsCopy[randomIndex];
                positionsCopy[randomIndex] = positionsCopy[i];
            }

            // 각 컵이 이동하는 과정을 Coroutine으로 실행시킵니다.
            for (int i = 0; i < cups.Length; i++)
            {
                float elapsedTime = 0f;
                Vector3 startPos = cups[i].transform.position;
                Vector3 endPos = targetPositions[i];

                // 목적지에 이미 다른 컵이 있는지 확인합니다.
                bool hasCollidingCup = false;
                foreach (var cup in cups)
                {
                    if (cup == cups[i]) continue; // 같은 컵은 건너뜀
                    if (cup.transform.position == endPos)
                    {
                        hasCollidingCup = true;
                        break;
                    }
                }

                if (!hasCollidingCup) // 충돌하는 컵이 없으면 현재 컵만 이동
                {
                    while (elapsedTime < shuffleDuration)
                    {
                        elapsedTime += Time.deltaTime;
                        float t = Mathf.Clamp01(elapsedTime / shuffleDuration);
                        cups[i].transform.position = Vector3.Lerp(startPos, endPos, t);
                        yield return null;
                    }
                }
                else // 충돌하는 컵이 있으면 두 컵 모두 이동
                {
                    foreach (var cup in cups)
                    {
                        if (cup.transform.position == endPos)
                        {
                            Vector3 tempPos = cup.transform.position;
                            while (elapsedTime < shuffleDuration)
                            {
                                elapsedTime += Time.deltaTime;
                                float t = Mathf.Clamp01(elapsedTime / shuffleDuration);
                                cup.transform.position = Vector3.Lerp(tempPos, startPos, t);
                                cups[i].transform.position = Vector3.Lerp(startPos, endPos, t);
                                yield return null;
                            }
                            cup.transform.position = startPos;
                            break;
                        }
                    }
                }

                // 마지막 위치로 정확하게 이동하도록 처리합니다.
                cups[i].transform.position = endPos;
                
            }
        }
        // foreach (GameObject cup in cups)
        // {
        //     BoxCollider collider = cup.GetComponent<BoxCollider>();
        //     collider.center = cup.transform.position;
        // }
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        Debug.Log("shuffle complete");
        isShuffling = false;

        yield return StartCoroutine(SelectCupAfterShuffleComplete());
    } 
    private IEnumerator SelectCupAfterShuffleComplete()
    {

        while (true)
        {
            // Shuffle 함수가 끝날 때까지 기다립니다.
            if (!isShuffling)
            {
                objectSelector.SelectCup();
                //공 삭제
                //makeAnswerScript.DestroyObject();
                // pauseBtn.interactable = false;
                shuffleOver = true;
                break;
            }

            yield return null;
        }

    }
    public void OnClick()
    {
        stBtn.interactable = false;
        pauseBtn.interactable = true;
        
        CorrectAnswerImage.SetActive(false);
        WrongAnswerImage.SetActive(false);

        Image image = stBtn.GetComponent<Image>();
        if (image != null && disabledImage != null)
        {
            image.sprite = disabledImage;
        }
        else{
            Debug.Log("disabled Image");
        }
        objectSelector = GetComponent<ObjectSelector>();
        generateCupScript = FindObjectOfType<GenerateCup>();
        // cups = generateCupScript.cups;
        // positions = generateCupScript.positions;
        if (clickCount < maxClicks)
        {
            // 정답생성
            int answerIndex = makeAnswerScript.makeBall();

            // 정답 보여주는 애니메이션 실행
            StartCoroutine(DisplayAnswerAndContinue(answerIndex));
        }
        else
        {
            stBtn.interactable = false;
            // 비활성화 이미지 설정
            if (image != null && disabledImage != null)
            {
                image.sprite = disabledImage;
            }
            else{
                Debug.Log("disabled Image");
            }
            // Debug.Log("game end");
        }
    }

    private IEnumerator DisplayAnswerAndContinue(int answerIndex)
    {
        // 정답 보여주는 애니메이션 실행
        yield return StartCoroutine(makeAnswerScript.displayAnswerFst("Cup" + answerIndex));

        // 정답 가리기
        //makeAnswerScript.HideObject();

        // 클릭 횟수가 최대 횟수 미만이면 작업을 수행
        clickCount++;

        // 작업 수행 코드
        // Shuffle coroutine을 실행하고, 코루틴을 변수에 저장합니다.
        shuffleCoroutine = StartCoroutine(Shuffle());

        if (objectSelector == null)
        {
            Debug.Log("objectSelector is null");
            yield break;
        }

        Debug.Log("cups length: " + cups.Length);

    }
    public void Cupinit()
    {
        for (int i = 0; i < cups.Length; i++)
        {
            positions[i] = cups[i].transform.position;
        }

        currentPositionIndex++;

        if (currentPositionIndex >= 5)
        {
            currentPositionIndex = 0;
        }

    }

    // P 버튼을 눌렀을 때 호출할 함수
    public void StopShuffle()
    {
        if (shuffleCoroutine != null)
        {
            // 코루틴 중지
            StopCoroutine(shuffleCoroutine);
            shuffleCoroutine = null;
            
            isShuffling = false; // isShuffling을 false로 설정하여 중지된 것을 표시
            
        }
        else{
            Debug.Log("cor null");
        }
    }
    // C 버튼을 눌렀을 때 호출할 함수
    public void StartShuffle()
    {
        if (!isShuffling)
        {
            // 중지된 코루틴을 다시 시작
            shuffleCoroutine = StartCoroutine(Shuffle());

        }
    }

    // P 버튼 클릭 이벤트 핸들러
    public void OnPButtonClick()
    {
        squareWindowPause.SetActive(true);
        pauseBtn.interactable = false;
        StopShuffle(); // 코루틴 중지
    }
    // C 버튼 클릭 이벤트 핸들러
    public void OnCButtonClick()
    {
        squareWindowPause.SetActive(false);
        pauseBtn.interactable = true;
        if(!shuffleOver) StartShuffle();
    }

}
