using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color HoverColor;                                       // цвет при наведении курсора на ноду
	public Color NotEnoughMoneyColor;                              // цвет недостаточности денег
	private Color _startColor;                                     // изначальный цвет ноды
	private Renderer _renderer;                                    // обьявление переменной отображения Render

	private Vector3 _positionOffset = new Vector3(0f, 0.5f, 0f);    // вектор смещения башни при спавне на ноде

	[HideInInspector]
	public GameObject Turret;
	[HideInInspector]
	public TurretBlueprint TurretBlueprint;
	[HideInInspector]
	public bool IsUpgraded = false;

	BuildManager BuildManager;                                     // обьявление BuildManager
	 
	private void Start ()
	{
		_renderer = GetComponent<Renderer>();                           // инициализация Render
		_startColor = _renderer.material.color;                         // инициализация изначального цывета

		BuildManager = BuildManager.Instance;                      // инициализация BuildManager
	}
	
	public Vector3 GetBuildPosition()
    {
		return transform.position + _positionOffset;
    }


	private void OnMouseDown()                                              
    {
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if(Turret != null)                                          // проверка наличия башни в ноде
        {
			BuildManager.SelectNode(this);                          // выбираем эту ноду 
			return;
        }

		if (!BuildManager.CanBuild)                                 // проверка возможности строительства 
			return;

		BuildTurret(BuildManager.GetTurrettoBuild());
    }

	private void BuildTurret(TurretBlueprint _blueprint)
	{
		if (PlayerStats.Money < _blueprint.cost)                // проверка на достаточность денег
		{
			Debug.Log("Not enough money to build");
			return;
		}

		PlayerStats.Money -= _blueprint.cost;                   // вычитание стоимости башни из денег игрока

		GameObject _turret = (GameObject)Instantiate(_blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		Turret = _turret;

		TurretBlueprint = _blueprint;                           // инициализировали переменную чертежа

		GameObject _effect = (GameObject)Instantiate(BuildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(_effect, 5f);                                   // уничтожить эффект через 5с

		Debug.Log($"Turret build! Money left: " + PlayerStats.Money);
	}

    public void UpgradeTurret()
    {
		if (PlayerStats.Money < TurretBlueprint.upgradeCost)                // проверка на достаточность денег
		{
			Debug.Log("Not enough money to upgrade");
			return;
		}

		PlayerStats.Money -= TurretBlueprint.upgradeCost;                   // вычитание стоимости башни из денег игрока

		Destroy(Turret);                                                    // уничтожение старой башни при апгрейде

		GameObject _turret = (GameObject)Instantiate(TurretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity); // ставим апгрейд туррели из собственного префаба
		Turret = _turret;

		GameObject _effect = (GameObject)Instantiate(BuildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(_effect, 5f); 

		IsUpgraded = true;

		Debug.Log($"Upgrade is done! Money left: " + PlayerStats.Money);
	}

	public void SellTurret()
    {
		PlayerStats.Money += TurretBlueprint.GetSellAmount();        // возврат денег за турель
		Destroy(Turret);
		TurretBlueprint = null;                                      // сброс чертежа
    }

	private void OnMouseEnter()                             
	{
		_renderer.materials[0].color = HoverColor;                        // придаём цвет выделенной ноде, на которую навели курсор

		if (EventSystem.current.IsPointerOverGameObject())          // проверка нет ли между курсором и нодой UI
			return;

		if (!BuildManager.CanBuild)
			return;

		if (BuildManager.HasMoney)
        {
            _renderer.material.color = HoverColor;
        }
        else
        {
			_renderer.material.color = NotEnoughMoneyColor;
        }

	}

	private void OnMouseExit()                                  
    {
		_renderer.materials[0].color = _startColor;                        // возвращаем ноде исходный цвет
    }
}
