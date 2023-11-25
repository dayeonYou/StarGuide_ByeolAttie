using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomSpawn : MonoBehaviour
{
    public GameObject[] prefabs; //찍어낼 게임 오브젝트를 넣어요
                                 //배열로 만든 이유는 게임 오브젝트를
                                 //다양하게 찍어내기 위해서 입니다
    private BoxCollider2D area;    //박스콜라이더의 사이즈를 가져오기 위함
    public int count = 5;      //찍어낼 게임 오브젝트 갯수
    private List<GameObject> gameObject = new List<GameObject>();
    public static int gold, crown, jewel, coin = 0;
    public Transform spawnPos;
    void Start()
    {
        area = GetComponent<BoxCollider2D>();

        for (int i = 0; i < count; ++i)//count 수 만큼 생성한다
        {
            Spawn();//생성 + 스폰위치를 포함하는 함수
            //각 아이템의 Tag를 보고 정답 개수를 미리 변수에 저장해놓기->정답인지 비교 가능
            
        }

        area.enabled = false;
    }
 
    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;
        //Transform size = area.size.transform;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;

    }

    private void Spawn()
    {
        int selection = Random.Range(0, prefabs.Length);

        GameObject selectedPrefab = prefabs[selection];

        Vector3 spawnPos = GetRandomPosition();//랜덤위치함수
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        //GameObject instance = Instantiate(selectedPrefab, spawnPos.position, spawnPos.rotation);
        instance.transform.localScale = new Vector3(0.15f, 0.15f, 0);
        Debug.Log("현재 랜덤선택된 아이템 Tag" + instance.tag);
        switch (instance.gameObject.tag)
        {
            case "gold":
                gold++;
                break;
            case "crown":
                crown++;
                break;
            case "jewel":
                jewel++;
                break;
            case "coin":
                coin++;
                break;
            default:
                Debug.Log("아이템이 잘못 생성되었습니다");
                break;
        }
        gameObject.Add(instance);
    }
}
