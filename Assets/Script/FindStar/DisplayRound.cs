using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 시 추가

public class DisplayRound : MonoBehaviour
{
    public Button stBtn;
    public Sprite disabledImage;

    public TextMeshProUGUI clickCountText; // TextMeshPro 사용 시 수정
    public int maxClicks = 6;

    public int clickCount = 0;

    void Start()
    {
        stBtn = GameObject.Find("stBtn").GetComponent<Button>();
        stBtn.onClick.AddListener(OnStBtnClick);

        // round text로 디스플레이 하고싶으면 주석 해제
        // clickCountText = GameObject.Find("RoundNumber").GetComponent<TextMeshProUGUI>(); // TextMeshPro 사용 시 수정
        // if(clickCountText==null){
        //     Debug.Log("nullText");
        // }
        // else{
        //     clickCountText.text = "ROUND : " + $"{clickCount}/{maxClicks}"; // 초기값 표시
        // }
    }

    void OnStBtnClick()
    {
        if (clickCount < maxClicks)
        {
            clickCount++;
            Debug.Log("clickcount: "+clickCount);
            
            // round text로 디스플레이 하고싶으면 주석 해제
            // 클릭 횟수 텍스트 업데이트
            // clickCountText.text = "ROUND : " + $"{clickCount}/{maxClicks}";

            // 작업 수행 코드
            // ...
        }
        else
        {
            stBtn.interactable = false;
            Image image = stBtn.GetComponent<Image>();
            if (image != null && disabledImage != null)
            {
                image.sprite = disabledImage;
            }
            else{
                Debug.Log("disabled Image");
            }
        }
    }
}
