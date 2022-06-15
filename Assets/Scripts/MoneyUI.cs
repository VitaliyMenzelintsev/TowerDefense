using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text MoneyText;

    private void Update()
    {
        MoneyText.text = "$"+ PlayerStats.Money.ToString();
    }
}
