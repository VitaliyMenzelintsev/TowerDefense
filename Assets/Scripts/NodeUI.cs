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
            UpgradeCost.text = "$" + this._target.TurretBlueprint.upgradeCost;   // ����� UI ��������/������� 
            UpgradeButton.interactable = true;                             // ������ ��������
        }
        else
        {
            UpgradeCost.text = "DONE";                                      // ������ ������
            UpgradeButton.interactable = false;                             // ������ ���������
        }

        SellAmount.text = "$" + this._target.TurretBlueprint.GetSellAmount();     // �������� � ������ ������ UI 

        UI.SetActive(true);      // ��� UI
    }

    public void Hide()
    {
        UI.SetActive(false);     // ���� UI
    }

    public void Upgrade()
    {
        _target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();        // �������� ���� ����� ����� ������
    }

    public void Sell()
    {
        _target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
