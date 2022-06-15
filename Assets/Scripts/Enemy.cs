using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float EnemyStartSpeed = 10f;

    [Header("Unity Stuff")]
    public Image HealthBar;                    

    [HideInInspector]
    public float Speed;
    public GameObject DeathEffect;

    private int _valueOfEnemy = 30;
    private float _startHealth = 100;           
    private float _health = 100;

    private void Start()
    {
        Speed = EnemyStartSpeed;                          // выставление скорости
        _health = _startHealth;
    }

    public void TakeDamage(float _amount)
    {
        _health -= _amount;

        HealthBar.fillAmount = _health / _startHealth;               // отображение здоровья 

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Slow(float _percent)
    {
        Speed = EnemyStartSpeed * (1f - _percent);           // расчёт замедления
    }

    private void Die()
    {
        PlayerStats.Money += _valueOfEnemy;                                                     // начисление денег за врага
        GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, Quaternion.identity); // спавн эффекта
        Destroy(effect, 3f);                                                                   // скип эффекта

        WaveSpawner.EnemiesAlive--;                                                         // уменьшение счётчика после смерти

        Destroy(gameObject);
    }
}
