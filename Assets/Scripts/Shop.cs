using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint StandartTurret;
    public TurretBlueprint MissileLauncher;
    public TurretBlueprint LaserBeamer;

    BuildManager buildManager;                            // ссылка на билдменеджер
    private void Start()
    {
        buildManager = BuildManager.Instance;             // инициализация билдменеджера
    }
    private void SelectStandartTurret()                    // выбор стандартной башни
    {
        Debug.Log("Standart Turret selected");
        buildManager.SelectTurretToBuild(StandartTurret); // выбрали стандартную турель из скрипта билдменеджер и теперь можно её строить
    }
    private void SelectMissileLauncher()                   // выбор ракетной башни
    {
        Debug.Log("Missile Launcher selected");
        buildManager.SelectTurretToBuild(MissileLauncher);  
    }

    private void SelectLaserBeamer()                      // выбор лазерной башни
    {
        Debug.Log("Laser Beamer selected");
        buildManager.SelectTurretToBuild(LaserBeamer);
    }

}
