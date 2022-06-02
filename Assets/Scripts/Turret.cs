using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]
	public float range = 15;

	[Header("Use Bullets")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;                                                // скорострельность
	private float fireCountDown = 0f;                                          // задержка между выстрелами

	[Header("Use Laser")]
	public bool useLaser;
	public LineRenderer lineRenderer;                                          // поле для рендеринга луча
	public int damageOverTime = 30;                                            // урон в секунду
	public float slowProcent = 0.5f;                                           // процент замедления

	[Header ("Unity Setup Fields")]
	public string enemyTag = "Enemy";

	public Transform partToRotate;                                             // определили поворачивающуюся деталь
	private float turnSpeed = 15f;                                             // скорость поворота башни

	public Transform firePoint;                                                // точка вылета пули (ствол башни)


	void Start ()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);                             // отслеживаем цель раз в 0.5 сек 
	}
	void UpdateTarget()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);    // создаём список игровых обьектов из врагов
		
		float shortestDistance = Mathf.Infinity;                               // кратчайшая дистанция - бесконечность
		
		GameObject nearestEnemy = null;                                        // ближайший враг не определён
		foreach (GameObject enemy in enemies)                                  // проходимся по списку врагов
        {
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);	
			if(distanceToEnemy < shortestDistance)           // если дистанция до врага меньше бесконечности, то ближайший враг из списка найден
            {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
            }
        }

		if(nearestEnemy != null && shortestDistance <= range)                  // проверка определения врага и кратчайшей дистанции 
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
			target = null;
        }
	}

	void Update ()
	{
		if (target == null)                                                // каждый кадр проверяем цель, если её нет, то успокаиваемся
		{
            if (useLaser)                                                  
            {
				if (lineRenderer.enabled)                                  // выключаем лазер, когда теряется цель
					lineRenderer.enabled = false;
            }
			return;
		}

		LockOnTarget();

        if (useLaser)
        {
			Laser();
        }
        else
        {
			if (fireCountDown <= 0)
			{
				Shoot();
				fireCountDown = 1f / fireRate;
			}

			fireCountDown -= Time.deltaTime;
		}
	}

	void LockOnTarget()
    {
		                                                                          // настраиваем угол поворота башни за целью
		Vector3 direction = target.position - transform.position;                 // вектор указывает на позицию цели
		Quaternion lookRotation = Quaternion.LookRotation(direction);             // угловая переменная с позицией направления

		// вектор угла поворота переводим в углы Эйлера. Lerp делает поворот плавным - нормализует

		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);             // задаём вращение верхушке башни (X и Z freeze)
	}

	void Laser()
    {
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);                  // урон от лазера
		targetEnemy.Slow(slowProcent);                                            // замедление от лазера

		if (!lineRenderer.enabled)                                                // включаем лазер
			lineRenderer.enabled = true;

		lineRenderer.SetPosition(0, firePoint.position);                          // лазер - это отрезок между двумя точками
		lineRenderer.SetPosition(1, target.position);                             // firepoint и target
    }

	void Shoot()
    {
		//создал GameObject, присвоил ему каждую заспавненную пулю
		GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		// обратился к скрипту Bullet, создал переменную bullet и присвоил ей компонент 
		Bullet bullet = bulletGameObject.GetComponent<Bullet>();
		if (bullet != null)                                                        // если пуля создана, то присваиваем ей цель
		{
			bullet.Seek(target);
		}
    }

	void OnDrawGizmosSelected()                                                    // обозначение дальности действия ВЫБРАННОЙ башни
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
    }
}
