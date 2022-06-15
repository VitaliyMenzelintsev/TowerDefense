using UnityEngine;

[System.Serializable]                 

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
