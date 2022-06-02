using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color hoverColor;                                       // цвет при наведении курсора на ноду
	public Color notEnoughMoneyColor;                              // цвет недостаточности денег
	private Color startColor;                                      // изначальный цвет ноды
	private Renderer rend;                                         // обьявление переменной отображения Render

	private Vector3 positionOffset = new Vector3(0f, 0.5f, 0f);    // вектор смещения башни при спавне на ноде

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	BuildManager buildManager;                                     // обьявление BuildManager
	 
	void Start ()
	{
		rend = GetComponent<Renderer>();                           // инициализация Render
		startColor = rend.material.color;                          // инициализация изначального цывета

		buildManager = BuildManager.instance;                      // инициализация BuildManager
	}
	
	public Vector3 GetBuildPosition()
    {
		return transform.position + positionOffset;
    }


	void OnMouseDown()                                              
    {
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if(turret != null)                                          // проверка наличия башни в ноде
        {
			buildManager.SelectNode(this);                          // выбираем эту ноду 
			return;
        }

		if (!buildManager.CanBuild)                                 // проверка возможности строительства 
			return;

		//buildManager.BuildTurretOn(this);

		BuildTurret(buildManager.GetTurrettoBuild());
    }

	void BuildTurret(TurretBlueprint blueprint)
	{
		if (PlayerStats.Money < blueprint.cost)                // проверка на достаточность денег
		{
			Debug.Log("Not enough money to build");
			return;
		}

		PlayerStats.Money -= blueprint.cost;                   // вычитание стоимости башни из денег игрока

		GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		turretBlueprint = blueprint;                           // инициализировали переменную чертежа

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);                                   // уничтожить эффект через 5с

		Debug.Log($"Turret build! Money left: " + PlayerStats.Money);
	}

    public void UpgradeTurret()
    {
		if (PlayerStats.Money < turretBlueprint.upgradeCost)                // проверка на достаточность денег
		{
			Debug.Log("Not enough money to upgrade");
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;                   // вычитание стоимости башни из денег игрока

		Destroy(turret);                                                    // уничтожение старой башни при апгрейде

		GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity); // ставим апгрейд туррели из собственного префаба
		turret = _turret;

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f); 

		isUpgraded = true;

		Debug.Log($"Upgrade is done! Money left: " + PlayerStats.Money);
	}

	public void SellTurret()
    {
		PlayerStats.Money += turretBlueprint.GetSellAmount();        // возврат денег за турель
		Destroy(turret);
		turretBlueprint = null;                                      // сброс чертежа
    }

    void OnMouseEnter ()                             
	{
		rend.materials[0].color = hoverColor;                        // придаём цвет выделенной ноде, на которую навели курсор

		if (EventSystem.current.IsPointerOverGameObject())          // проверка нет ли между курсором и нодой UI
			return;

		if (!buildManager.CanBuild)
			return;

		if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
			rend.material.color = notEnoughMoneyColor;
        }

	}

	void OnMouseExit()                                  
    {
		rend.materials[0].color = startColor;                        // возвращаем ноде исходный цвет
    }
}
