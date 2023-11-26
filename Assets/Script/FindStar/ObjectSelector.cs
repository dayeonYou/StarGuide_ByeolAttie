using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 시 추가
using Unity.Burst.CompilerServices;

public class ObjectSelector : MonoBehaviour
{
    public bool isSelecting = false;
    ObjectSelector objectSelector;
    GenerateCup generateCupScript;
    ShuffleCups shuffleCupsScript;
    MakeAnswer makeAnswerScript;
    Find_DetectCollision Find_DetectCollision;

    public GameObject[] cups;
    public Button stBtn;

    public GameObject Panel_selectMessage;
    public float displayTime = 1f;

    public Sprite abledImage;
    public void SelectCup()
    {
        stBtn = GameObject.Find("stBtn").GetComponent<Button>();

        generateCupScript = FindObjectOfType<GenerateCup>();
        // cups = generateCupScript.cups;
        objectSelector = FindObjectOfType<ObjectSelector>();
        shuffleCupsScript = FindObjectOfType<ShuffleCups>();
        makeAnswerScript = FindObjectOfType<MakeAnswer>();
        Find_DetectCollision = FindObjectOfType<Find_DetectCollision>();

        foreach (GameObject cup in cups)
        {
            BoxCollider collider = cup.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            if (collider != null && collider.enabled)
            {
                // collider가 활성화되어 있음
                Debug.Log("collider is enabled for cup: " + cup.name);
            }
            else
            {
                // collider가 비활성화되어 있음 또는 collider가 없음
                Debug.Log("collider is disabled for cup: " + cup.name);
            }
        }

        //Debug.Log("Selecting object");
        StartCoroutine(ActivateTextCoroutine());

        isSelecting = true;
    }

    private void Update()
    {   
        /*
        if (isSelecting && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked on: " + hit.transform.name); // 오브젝트의 이름 출력
                if (shuffleCupsScript.maxClicks > shuffleCupsScript.clickCount)
                {
                    stBtn.interactable = true;
                    Image image = stBtn.GetComponent<Image>();
                    if (image != null && abledImage != null)
                    {
                        image.sprite = abledImage;
                    }
                    else
                    {
                        Debug.Log("able Image error");
                    }
                    bool RW = makeAnswerScript.checkAnswer(hit.transform.name);
                    Debug.Log(RW);

                }
                else if (shuffleCupsScript.maxClicks == shuffleCupsScript.clickCount)
                {
                    bool RW = makeAnswerScript.checkAnswer(hit.transform.name);
                    Debug.Log(RW);
                }
                isSelecting = false;

            }
            else
            {
                Debug.Log("No object selected");
                Debug.Log(ray);
                //다시 선택하라는 텍스트 출력
                //..
                isSelecting = true;
            }

        }
        */
    }
    private System.Collections.IEnumerator ActivateTextCoroutine()
    {
        Panel_selectMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        Panel_selectMessage.gameObject.SetActive(false);
    }

    public void ClickCup()
    {
        string hitobject = Find_DetectCollision.currentlyGazedObject.name;

        if (shuffleCupsScript.maxClicks > shuffleCupsScript.clickCount)
        {
            stBtn.interactable = true;
            Image image = stBtn.GetComponent<Image>();
            if (image != null && abledImage != null)
            {
                image.sprite = abledImage;
            }
            else
            {
                Debug.Log("able Image error");
            }
            bool RW = makeAnswerScript.checkAnswer(hitobject);
            Debug.Log(RW);

        }
        else if (shuffleCupsScript.maxClicks == shuffleCupsScript.clickCount)
        {
            bool RW = makeAnswerScript.checkAnswer(hitobject);
            Debug.Log(RW);
        }
        isSelecting = false;

    }
}
