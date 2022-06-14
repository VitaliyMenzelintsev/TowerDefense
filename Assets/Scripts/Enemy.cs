using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [Header("Unity Stuff")]
    public Image healthBar;                    


    [HideInInspector]
    public float speed;
    public GameObject deathEffect;
    public int valueOfEnemy = 30;               
    public float startHealth = 100;           
    private float health = 100;


    private void Start()
    {
        speed = startSpeed;                          // выставление скорости
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;               // отображение здоровья 

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float procent)
    {
        speed = startSpeed * (1f - procent);           // расчёт замедления
    }

    void Die()
    {
        PlayerStats.Money += valueOfEnemy;                                                     // начисление денег за врага
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity); // спавн эффекта
        Destroy(effect, 3f);                                                                   // скип эффекта

        WaveSpawner.EnemiesAlive--;                                                         // уменьшение счётчика после смерти

        Destroy(gameObject);
    }
}
