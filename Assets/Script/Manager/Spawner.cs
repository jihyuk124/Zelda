using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    // Inspector���� ũ�⸦ ������ �� �ִ� x, y, z ��
    [SerializeField] private Vector3 boxSize = new Vector3(1, 1, 1);
    [SerializeField] private float height = 0;
    [SerializeField] private Color gizmoColor = Color.green;  // Gizmo ���� ����
    [SerializeField] private bool showGizmo = true;  // Gizmo ǥ�� ����
    [SerializeField] private float nearDistance = 1.0f;

    public GameObject flowerPrefab; // ��ġ�� ���� ������
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
        if (showGizmo)  // Gizmo ǥ�� ���ο� ���� �׸���
        {
            // Gizmo ���� ����
            Gizmos.color = gizmoColor;

            // ���� ������Ʈ ��ġ���� ũ�⿡ ���� �簢�� �Ǵ� �ڽ��� �׸�
            Gizmos.DrawCube(transform.position, new Vector3(boxSize.x * 2, boxSize.y * 2, boxSize.z * 2));
        }
    }
}
