using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]                 //отображение всех переменных в инспекторе

public class TurretBlueprint          // чертежи башен
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()       // стоимость продажи турели
    {
        return cost / 2;
    }
}
