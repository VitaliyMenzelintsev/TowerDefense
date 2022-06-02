using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;

    public GameObject ui;

    public Text upgradeCost;
    public Text sellAmount;
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;   // вывод UI апгрейда/продажи 
            upgradeButton.interactable = true;                             // кнопка включена
        }
        else
        {
            upgradeCost.text = "DONE";                                      // замена текста
            upgradeButton.interactable = false;                             // кнопка выключена
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();     // привязка к тексту внутри UI 

        ui.SetActive(true);      // вкл UI
    }

    public void Hide()
    {
        ui.SetActive(false);     // выкл UI
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();        // скрываем меню сразу после выбора
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
