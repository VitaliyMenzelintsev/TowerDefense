using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;                   // ���������� ���� ���� (�����) ���������
    private int wavepointIndex = 0;             // ������ ����� �������� �� ������
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = WayPoints.points[0];           // �������������� target ��� ������ ����� ��������
    }

    void Update()
    {
        Vector3 directionToPoint = target.position - transform.position;     // ������ ����������� � ����� ��������

        transform.Translate(directionToPoint.normalized * enemy.speed * Time.deltaTime, Space.World);    // �������� �� �������

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)   // ������ ������������ ����� ��������
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;        // ���������� �������� �� ������� ������ ����
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= WayPoints.points.Length - 1)                    // ���� ����� �������� ������ ��� - ����������� �������
        {
            EndPath();
            return;
        }
        wavepointIndex++;                                                     // ��������� ����� �������� ���������� ����������

        target = WayPoints.points[wavepointIndex];                            // ���� - ���������� ����� ��������
    }

    void EndPath()
    {
        PlayerStats.Lives--;                                                  // ��������� �����, ����� ���� ��������� ������ ����
        WaveSpawner.EnemiesAlive--;                                           // ���������� �������� ����� ���������� END
        Destroy(gameObject);
    }
}
