using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float speed = 70f;
    public int damage = 50;

    public float explosionRadius = 0;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);                                     // если у пули нет цели - уничтожаем её
            return;
        }

        Vector3 direction = target.position - transform.position;    // вектор от пули до цели
        float distanceThisFrame = speed * Time.deltaTime;            // расстояние, которое пролетает пуля

        if (direction.magnitude <= distanceThisFrame)                 // длина вектора до цели <= расстояние до цели
        {
            HitTarget();                                             // пуля попала в цель и запуск метода попадания
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);                     //движение пули
        transform.LookAt(target);                                    // разворот п оси Х в сторону цели

    }
    void HitTarget()
    {
        GameObject effectInstatiate = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstatiate, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);                                           // уничтожение пули
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy enemyBullet = enemy.GetComponent<Enemy>();

        if (enemyBullet != null)
        {
            enemyBullet.TakeDamage(damage);
        }         
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
