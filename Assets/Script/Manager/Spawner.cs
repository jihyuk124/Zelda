using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    // Inspector에서 크기를 수정할 수 있는 x, y, z 값
    [SerializeField] private Vector3 boxSize = new Vector3(1, 1, 1);
    [SerializeField] private float height = 0;
    [SerializeField] private Color gizmoColor = Color.green;  // Gizmo 색상 설정
    [SerializeField] private bool showGizmo = true;  // Gizmo 표시 여부
    [SerializeField] private float nearDistance = 1.0f;

    public GameObject flowerPrefab; // 배치할 꽃의 프리팹
    public int numberOfFlowers = 10;
    private float mapSize = 15f;

    void Start()
    {
        SpawnFlowers();
    }

    void SpawnFlowers()
    {
        for (int i = 0; i < numberOfFlowers; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            if (GetRandomPositionOnNavMesh(ref randomPosition))
            {
                Instantiate(flowerPrefab, randomPosition, Quaternion.identity);
            }
        }
    }

    bool GetRandomPositionOnNavMesh(ref Vector3 outValue)
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(transform.position.x -boxSize.x, transform.position.x + boxSize.x),
            height,
            Random.Range(transform.position.z - boxSize.z, transform.position.z + boxSize.z)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, nearDistance, NavMesh.AllAreas))
        {
            if (hit.position.y < height)
            {
                return false;
            }
            outValue = new Vector3(hit.position.x, height, hit.position.z);
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)  // Gizmo 표시 여부에 따라 그리기
        {
            // Gizmo 색상 설정
            Gizmos.color = gizmoColor;

            // 현재 오브젝트 위치에서 크기에 맞춰 사각형 또는 박스를 그림
            Gizmos.DrawCube(transform.position, new Vector3(boxSize.x * 2, boxSize.y * 2, boxSize.z * 2));
        }
    }
}
