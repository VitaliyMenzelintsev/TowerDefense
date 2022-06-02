using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;                              // количество живых врагов

    public Wave[] waves;                                             // создание массива волн

    public float timeBetweenWaves = 5.0f;                            // время между волнами
    private float countDown = 2.0f;                                  // обратный отсчёт

    public Transform spawnPoint;

    public Text waveCountDownText;

    private int waveIndex = 0;                                       // номер волны

    void Start()
    {

    }
    void Update()
    {
        if(EnemiesAlive > 0)                                           // не спавним врагов, если волна не закончилась
        {
            return;
        }

        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());                           
            countDown = timeBetweenWaves;
            return;
        }
        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);          // зажимаем значение обратного отсчёта в пределах от 0 до бесконечности

        waveCountDownText.text = string.Format("TIME:" + "{0:00.00}", countDown);  // выводим строку заданного формата
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;                                           // счётчик преодолённых раундов(волн) для Game Over

        Wave wave = waves[waveIndex];                                   

        for (int i = 0; i < wave.count; i++)                             // спавним столько врагов, какой номер волны
        {
            SpawnEnemy(wave.enemy);                                      // возможность передать в метод конкретный тип врагов
            yield return new WaitForSeconds(1f / wave.rate);             // время между врагами внутри волны
        }

        waveIndex++;

        if(waveIndex == waves.Length)
        {
            Debug.Log("Level Won!");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;                                                   // прирост счётчика после спавна
    }
}
