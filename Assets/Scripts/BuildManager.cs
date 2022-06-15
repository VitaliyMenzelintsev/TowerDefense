using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;                           // делаем синглтон
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("more than 1 buildmanager here");
            return;
        }
        Instance = this;
    }

    public GameObject StandartTurretPrefab;
    public GameObject MissileLauncherPrefab;
    public GameObject LaserBeamerPrefab;
    public GameObject BuildEffect;

    public NodeUI NodeUI;

    private TurretBlueprint _turretToBuild;                                           // варианты, какую башню построить
    private Node _selectedNode;

    public bool CanBuild { get { return _turretToBuild != null; } }                   // можно ли строить (только чтение)
    public bool HasMoney { get { return PlayerStats.Money >= _turretToBuild.cost; } } // хватает ли денег

    public void SelectNode(Node _node)
    {
        if (_selectedNode == _node)
        {
            DeselectNode();
            return;
        }

        _selectedNode = _node;
        _turretToBuild = null;

        NodeUI.SetTarget(_node);
    }

    public void DeselectNode()                // отмена выбора ноды
    {
        _selectedNode = null;
        NodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint _turret)
    {
        _turretToBuild = _turret;
        DeselectNode();                       // выкл nodeUI, когда нажимаем на UI выбора турели
    }

    public TurretBlueprint GetTurrettoBuild()
    {
        return _turretToBuild;
    }
}
