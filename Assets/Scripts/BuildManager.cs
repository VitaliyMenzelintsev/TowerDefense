using UnityEngine;

public class BuildManager : MonoBehaviour 
{
    public static BuildManager instance;                           // делаем синглтон
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("more than 1 buildmanager here");
            return;
        }
        instance = this;
    }

    public GameObject standartTurretPrefab;       
    public GameObject missileLauncherPrefab;
    public GameObject LaserBeamerPrefab;

    public GameObject buildEffect;
   
    private TurretBlueprint turretToBuild;                                           // варианты, какую башню построить
    private Node selectedNode;
   
    public NodeUI nodeUI; 

    public bool CanBuild { get { return turretToBuild != null; } }                   // можно ли строить (только чтение)
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } } // хватает ли денег

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;    
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()                // отмена выбора ноды
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();                       // выкл nodeUI, когда нажимаем на UI выбора турели
    }

    public TurretBlueprint GetTurrettoBuild()
    {
        return turretToBuild;
    }
}
