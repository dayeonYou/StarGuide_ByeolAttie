using System.Collections;
using UnityEngine;

public class GenerateCup : MonoBehaviour
{
    public int numOfCups = 5; // 생성할 컵 개수
    public GameObject cupPrefab; // 컵 프리팹

    public GameObject[] cups; // 생성한 컵 오브젝트들을 담을 배열 변수
    public Vector3[] positions; // 컵 위치를 저장할 배열 변수
    // public float distanceBetweenObjects = 2.5f; // 오브젝트 사이의 거리
    // public float FrsXPosition = -5; // 오브젝트 사이의 거리
    public float[] XPosition;
    public float[] YPosition;
    public float[] scaleOfCup;
    
    void Awake()
    {
        // XPosition = new float[7];
        // YPosition = new float[7];

        cups = new GameObject[numOfCups];
        positions = new Vector3[numOfCups];

        // 컵 위치 계산
        for (int i = 0; i < numOfCups; i++)
        {
            GameObject newCup = Instantiate(cupPrefab, transform.position, Quaternion.identity);
            newCup.name = "Cup" + i.ToString();
            cups[i] = newCup;
            cups[i].layer = LayerMask.NameToLayer("cups");

            newCup.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // 컵 오브젝트에 collider 컴포넌트 추가
            BoxCollider collider = newCup.AddComponent<BoxCollider>();
            // collider 컴포넌트 크기 조정
            collider.size = new Vector3(13.13f, 12.57f, 0.2f);
            // collider 컴포넌트 위치 조정
            collider.center = new Vector3(0, 0, 0);
            // 첫 번째 컵 위치 지정
            if (i == 0)
            {
                positions[i] = new Vector3(XPosition[0], YPosition[0], transform.position.z);
            }
            // 첫 번째 컵 이후의 컵 위치 지정
            else
            {
                positions[i] = new Vector3(XPosition[i], YPosition[i], transform.position.z);
            }
            newCup.transform.position = positions[i];
            newCup.transform.localScale = new Vector3(scaleOfCup[0], scaleOfCup[1], scaleOfCup[2]);

        }
    }
   
}
