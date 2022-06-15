using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;                   // переменная цель типа (класс) Трансформ
    private int _wavepointIndex = 0;             // индекс точки маршрута из списка
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();

        _target = WayPoints.Points[0];           // ицинциализация target как списка точек маршрута
    }

    private void Update()
    {
        Vector3 _directionToPoint = _target.position - transform.position;     // вектор направления к точке маршрута

        transform.Translate(_directionToPoint.normalized * _enemy.Speed * Time.deltaTime, Space.World);    // движение по вектору

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)   // радиус срабатывания точки маршрута
        {
            GetNextWaypoint();
        }

        _enemy.Speed = _enemy.EnemyStartSpeed;        // сбрасываем скорость на базовую каждый кадр
    }

    private void GetNextWaypoint()
    {
        if (_wavepointIndex >= WayPoints.Points.Length - 1)                    // если точек маршрута больше нет - уничтожение обьекта
        {
            EndPath();
            return;
        }
        _wavepointIndex++;                                                     // следующая точка маршрута становится актуальной

        _target = WayPoints.Points[_wavepointIndex];                            // цель - актуальная точка маршрута
    }

    private void EndPath()
    {
        PlayerStats.Lives--;                                                  // уменьшаем жизни, когда враг достигает финала пути
        WaveSpawner.EnemiesAlive--;                                           // уменьшение счётчика после достижения END
        Destroy(gameObject);
    }
}
