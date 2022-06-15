using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node _target;

    public GameObject UI;
    public Text UpgradeCost;
    public Text SellAmount;
    public Button UpgradeButton;

    public void SetTarget(Node _target)
    {
        this._target = _target;

        transform.position = this._target.GetBuildPosition();

        if (!this._target.IsUpgraded)
        {
            UpgradeCost.text = "$" + this._target.TurretBlueprint.upgradeCost;   // вывод UI апгрейда/продажи 
            UpgradeButton.interactable = true;                             // кнопка включена
        }
        else
        {
            UpgradeCost.text = "DONE";                                      // замена текста
            UpgradeButton.interactable = false;                             // кнопка выключена
        }

        SellAmount.text = "$" + this._target.TurretBlueprint.GetSellAmount();     // привязка к тексту внутри UI 

        UI.SetActive(true);      // вкл UI
    }

    public void Hide()
    {
        UI.SetActive(false);     // выкл UI
    }

    public void Upgrade()
    {
        _target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();        // скрываем меню сразу после выбора
    }

    public void Sell()
    {
        _target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
