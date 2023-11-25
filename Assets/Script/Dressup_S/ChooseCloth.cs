using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ChooseCloth : MonoBehaviour
{
    public static GameObject instance; //복제품 만들기
    public static string h_instance, s_instance, p_instance, ss_instance; //Heart0씬에서 여전히 남아있는 dontdestroy오브젝트 이름 기억
    public GameObject res; //삭제할 오브젝트
    public static int shirt, hat, pant, shoes = 0; //복제품이 한 부위당 하나씩만 생성되도록 0,1값 반복
    public static int s_before, h_before, p_before, ss_before = 0; //전 색상 기억해두기
    int num = 0; //몇 번째 색상일지 비교
    string name = "";
    public bool select = false;
    public string scene = ""; // 현재 씬이 Heart1이면 instance를 load후 지우지 말아야함.
    public Transform spawnPoint;
    public void Onclick()
    {
        Debug.Log("shirt"+shirt);
        Debug.Log("shoes" + shoes);
        Debug.Log("pant" + pant);
        Debug.Log("hat" + hat);
        scene = SceneManager.GetActiveScene().name;
        GetPosition(); //��ġ�����Լ�
    }
    public void GetPosition()
    {
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
        Debug.Log("click한거 이름 : "+clickBtn.name);
        string tag = clickBtn.gameObject.tag;
        //Debug.Log(tag);
        switch (tag)
        {
            case "shirt":
                name = "shirt1";
                break;
            case "shoes":
                name = "shoes1";
                break;
            case "pant":
                name = "pant1";
                break;
            case "hat":
                name = "hat1";
                break;
            default:
                Debug.Log("클릭한 버튼이 인식되지 않았습니다.");
                break;
        }
        instance = Instantiate(clickBtn.transform.GetChild(0).gameObject, spawnPoint.position, spawnPoint.rotation, GameObject.Find("Canvas").transform.Find(name).gameObject.transform);
        Debug.Log("instance 이름 :" + instance.name);
        if (instance.CompareTag("shirt"))
        {
            instance.transform.localScale = new Vector3(1.26f, 2.2f, 0);
            //복제품 tag는 shirt였지만 복제품 이름 (0,1,2,3)에 따라 tag를 s_0, s_1, s_2, s_3으로 수정
            //Delete("s_"+num)로 오브젝트 지우자
            if (shirt == 1)
            {
                Debug.Log("삭제할 전 색상" + s_before);
                Delete("s_" + s_before); shirt--;
            }
            for (num=0; num<=6; num++)
            {
                if (instance.name == "shirtImage " + "("+num+")(Clone)")
                {
                    Debug.Log("shirt:" + num + "번");
                    instance.tag = "s_" + num;
                    s_before = num; //다른 색상 선택하기 전의 색상
                }
            }
            shirt++;
        }
        else if (instance.CompareTag("shoes"))
        {
            instance.transform.localScale = new Vector3(1.35f, 2.65f, 0);
            if (shoes == 1)
            {
                Debug.Log("삭제할 전 색상" + ss_before);
                Delete("ss_" + ss_before); shoes--;
            }
            for (num = 0; num <= 6; num++)
            {
                if (instance.name == "shoesImage " + "(" + num + ")(Clone)")
                {
                    Debug.Log("shoes:" + num + "번");
                    instance.tag = "ss_" + num;
                    ss_before = num; //다른 색상 선택하기 전의 색상
                }
            }
            shoes++;
        }
        else if (instance.CompareTag("pant"))
        {
            instance.transform.localScale = new Vector3(0.8f, 1.2f, 0);
            if (pant == 1)
            {
                Debug.Log("삭제할 전 색상" + p_before);
                Delete("p_" + p_before); pant--;
            }
            for (num = 0; num <= 6; num++)
            {
                if (instance.name == "pantImage " + "(" + num + ")(Clone)")
                {
                    Debug.Log("pant:" + num + "번");
                    instance.tag = "p_" + num;
                    p_before = num; //다른 색상 선택하기 전의 색상
                }
            }
            pant++;
        }
        else if (instance.CompareTag("hat"))
        {
            instance.transform.localScale = new Vector3(1.2f, 5.7f, 0);
            if (hat == 1)
            {
                Debug.Log("삭제할 전 색상" + h_before);
                Delete("h_" + h_before); hat--;
            }
            for (num = 0; num <= 6; num++)
            {
                if (instance.name == "hatImage " + "(" + num + ")(Clone)")
                {
                    Debug.Log("hat:" + num + "번");
                    instance.tag = "h_" + num;
                    h_before = num; //다른 색상 선택하기 전의 색상
                }
            }
            hat++;
        }
    }
    public void Delete(string item)
    {
        Debug.Log("선택된거 tag" + item);
        res = GameObject.FindWithTag(item); 
        //처음에 res가 hat이 자꾸 나온 이유 : RandomCloth.cs에서 tag를 h_0이런식으로
        //바꿔버리는 코드를 불필요하게 작성해서 정답옷이 tag가 복제품 tag랑 겹쳐버렸기 때문.
        //tag수정 코드 빼니까 해결! 
        Debug.Log("삭제할 것 : "+res);
        Destroy(res);
    }
}
