using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;                   // ���������� ���� ���� (�����) ���������
    private int _wavepointIndex = 0;             // ������ ����� �������� �� ������
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();

        _target = WayPoints.Points[0];           // �������������� target ��� ������ ����� ��������
    }

    private void Update()
    {
        Vector3 _directionToPoint = _target.position - transform.position;     // ������ ����������� � ����� ��������

        transform.Translate(_directionToPoint.normalized * _enemy.Speed * Time.deltaTime, Space.World);    // �������� �� �������

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)   // ������ ������������ ����� ��������
        {
            GetNextWaypoint();
        }

        _enemy.Speed = _enemy.EnemyStartSpeed;        // ���������� �������� �� ������� ������ ����
    }

    private void GetNextWaypoint()
    {
        if (_wavepointIndex >= WayPoints.Points.Length - 1)                    // ���� ����� �������� ������ ��� - ����������� �������
        {
            EndPath();
            return;
        }
        _wavepointIndex++;                                                     // ��������� ����� �������� ���������� ����������

        _target = WayPoints.Points[_wavepointIndex];                            // ���� - ���������� ����� ��������
    }

    private void EndPath()
    {
        PlayerStats.Lives--;                                                  // ��������� �����, ����� ���� ��������� ������ ����
        WaveSpawner.EnemiesAlive--;                                           // ���������� �������� ����� ���������� END
        Destroy(gameObject);
    }
}
