using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Lives;
    public static int Rounds;

    private int _startMoney = 400;
    private int _startLives = 20;
    
    private void Start()
    {
        Money = _startMoney;
        Lives = _startLives;

        Rounds = 0;
    }
}
