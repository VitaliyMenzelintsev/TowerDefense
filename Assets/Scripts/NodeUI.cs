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
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;   // ����� UI ��������/������� 
            upgradeButton.interactable = true;                             // ������ ��������
        }
        else
        {
            upgradeCost.text = "DONE";                                      // ������ ������
            upgradeButton.interactable = false;                             // ������ ���������
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();     // �������� � ������ ������ UI 

        ui.SetActive(true);      // ��� UI
    }

    public void Hide()
    {
        ui.SetActive(false);     // ���� UI
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();        // �������� ���� ����� ����� ������
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
