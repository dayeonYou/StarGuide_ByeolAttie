using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public int numTargets = 5;
    public float spawnRange = 10f;
    public float minDistance = 3f;

    public bool drawSpawnArea = true;
    public Color spawnAreaColor = Color.red;

    private Camera mainCamera;
    private Rect spawnArea;

    private Rect[] spawnAreasHalvesUpperThirds;
    private Rect[] spawnAreasHalvesLowerHalf;
    private Rect[] spawnAreasHalves;

    private void Start()
    {
        mainCamera = Camera.main;
        CalculateSpawnArea();
    }

    public void OnClick()
    {
        SpawnTargets();
    }

    private void CalculateSpawnArea()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector2 cameraPosition = mainCamera.transform.position;
        Vector2 spawnAreaCenter = cameraPosition;
        spawnAreaCenter.y -= cameraHeight / 2f;

        cameraWidth -= 1f; // 가로 줄임

        float totalSpawnWidthUpper = cameraWidth / 3f; // 위쪽은 세 개의 지역으로 나눔
        float totalSpawnWidthLower = cameraWidth / 2f; // 아래쪽은 두 개의 지역으로 나눔
        float totalSpawnHeight = (cameraHeight - 3f) / 2f;

        spawnAreasHalvesUpperThirds = new Rect[3];
        for (int i = 0; i < spawnAreasHalvesUpperThirds.Length; i++)
            spawnAreasHalvesUpperThirds[i] =
                new Rect(spawnAreaCenter.x + ((i + 1) * totalSpawnWidthUpper) - 
                (spawnRange + totalSpawnWidthUpper) - (spawnRange + cameraWidth / 2f),
                         spawnAreaCenter.y + totalSpawnHeight + 1f,
                         totalSpawnWidthUpper,
                         totalSpawnHeight);

        spawnAreasHalvesLowerHalf = new Rect[2];
        for (int i = 0; i < spawnAreasHalvesLowerHalf.Length; i++)
            spawnAreasHalvesLowerHalf[i] =
                new Rect(spawnAreaCenter.x + ((i + 1) * totalSpawnWidthLower) - 
                (spawnRange + totalSpawnWidthLower) - (spawnRange + cameraWidth / 2f),
                         spawnAreaCenter.y + 1f,
                         totalSpawnWidthLower,
                         totalSpawnHeight);
    }


    void OnDrawGizmos()
    {
        if (spawnAreasHalvesUpperThirds != null)
        {
            Gizmos.color = Color.red;
            foreach (Rect area in spawnAreasHalvesUpperThirds)
            {
                Gizmos.DrawWireCube(area.center, new Vector3(area.width, area.height, 0));
            }
        }

        if (spawnAreasHalvesLowerHalf != null)
        {
            Gizmos.color = Color.blue;
            foreach (Rect area in spawnAreasHalvesLowerHalf)
            {
                Gizmos.DrawWireCube(area.center, new Vector3(area.width, area.height, 0));
            }
        }
    }

    private void SpawnTargets()
    {
        Rect[] allSpawnAreas = spawnAreasHalvesUpperThirds.Concat(spawnAreasHalvesLowerHalf).ToArray();

        foreach (Rect spawnArea in allSpawnAreas)
        {
            int attempts = 10; // 최대 시도 횟수

            while (attempts > 0)
            {
                Vector2 randomPosition = GetRandomPosition(spawnArea);
                if (IsDistanceValid(randomPosition))
                {
                    Instantiate(targetPrefab, randomPosition, Quaternion.identity);
                    break;
                }
                else
                {
                    attempts--; // Retry spawning for this index
                }
            }

            if (attempts == 0)
            {
                Debug.LogWarning("Failed to spawn target with enough distance.");
            }
        }
    }

    private Vector2 GetRandomPosition(Rect spawnArea)
    {
        float randomX = UnityEngine.Random.Range(spawnArea.xMin, spawnArea.xMax);
        float randomY = UnityEngine.Random.Range(spawnArea.yMin, spawnArea.yMax);

        return new Vector2(randomX, randomY);
    }

    private bool IsDistanceValid(Vector2 position)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(position, target.transform.position);
            if (distance < minDistance)
            {
                return false;
            }
        }

        // 추가: 이전에 생성된 모든 타겟과의 거리를 확인하여 최소 간격보다 작은지 여부를 검사
        float minDistanceSqr = minDistance * minDistance;
        foreach (GameObject target in targets)
        {
            float distanceSqr = (position - (Vector2)target.transform.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                return false;
            }
        }

        return true;
    }
}