using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;                            // ссылка на билдменеджер
    void Start()
    {
        buildManager = BuildManager.instance;             // инициализация билдменеджера
    }
    public void SelectStandartTurret()                    // выбор стандартной башни
    {
        Debug.Log("Standart Turret selected");
        buildManager.SelectTurretToBuild(standartTurret); // выбрали стандартную турель из скрипта билдменеджер и теперь можно её строить
    }
    public void SelectMissileLauncher()                   // выбор ракетной башни
    {
        Debug.Log("Missile Launcher selected");
        buildManager.SelectTurretToBuild(missileLauncher);  
    }

    public void SelectLaserBeamer()                      // выбор лазерной башни
    {
        Debug.Log("Laser Beamer selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }

}
