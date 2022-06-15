using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
                               
    private int _waveIndex = 0;                                       // номер волны
    private float _timeBetweenWaves = 5.0f;                            // время между волнами
    private float _countDown = 2.0f;                                  // обратный отсчёт
    public static int EnemiesAlive = 0; 
    public Transform SpawnPoint;
    public Text WaveCountDownText;
    public Wave[] Waves;

    private void Update()
    {
        if(EnemiesAlive > 0)                                           // не спавним врагов, если волна не закончилась
        {
            return;
        }

        if (_countDown <= 0)
        {
            StartCoroutine(SpawnWave());                           
            _countDown = _timeBetweenWaves;
            return;
        }
        _countDown -= Time.deltaTime;

        _countDown = Mathf.Clamp(_countDown, 0f, Mathf.Infinity);          // зажимаем значение обратного отсчёта в пределах от 0 до бесконечности

        WaveCountDownText.text = string.Format("TIME:" + "{0:00.00}", _countDown);  // выводим строку заданного формата
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;                                           // счётчик преодолённых раундов(волн) для Game Over

        Wave wave = Waves[_waveIndex];                                   

        for (int i = 0; i < wave.Count; i++)                             // спавним столько врагов, какой номер волны
        {
            SpawnEnemy(wave.Enemy);                                      // возможность передать в метод конкретный тип врагов
            yield return new WaitForSeconds(1f / wave.Rate);             // время между врагами внутри волны
        }

        _waveIndex++;

        if(_waveIndex == Waves.Length)
        {
            Debug.Log("Level Won!");
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
        EnemiesAlive++;                                                   // прирост счётчика после спавна
    }
}
