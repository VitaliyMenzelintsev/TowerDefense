using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;                   // переменная цель типа (класс) Трансформ
    private int wavepointIndex = 0;             // индекс точки маршрута из списка
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = WayPoints.points[0];           // ицинциализация target как списка точек маршрута
    }

    void Update()
    {
        Vector3 directionToPoint = target.position - transform.position;     // вектор направления к точке маршрута

        transform.Translate(directionToPoint.normalized * enemy.speed * Time.deltaTime, Space.World);    // движение по вектору

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)   // радиус срабатывания точки маршрута
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;        // сбрасываем скорость на базовую каждый кадр
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= WayPoints.points.Length - 1)                    // если точек маршрута больше нет - уничтожение обьекта
        {
            EndPath();
            return;
        }
        wavepointIndex++;                                                     // следующая точка маршрута становится актуальной

        target = WayPoints.points[wavepointIndex];                            // цель - актуальная точка маршрута
    }

    void EndPath()
    {
        PlayerStats.Lives--;                                                  // уменьшаем жизни, когда враг достигает финала пути
        WaveSpawner.EnemiesAlive--;                                           // уменьшение счётчика после достижения END
        Destroy(gameObject);
    }
}
