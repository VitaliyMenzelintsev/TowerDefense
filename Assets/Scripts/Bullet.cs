using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform Target;
    private float _speed = 70f;
    private int _damage = 50;
    private float _explosionRadius = 0;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        this.Target = _target;
    }

    private void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);                                     // если у пули нет цели - уничтожаем её
            return;
        }

        Vector3 _direction = Target.position - transform.position;    // вектор от пули до цели
        float _distanceThisFrame = _speed * Time.deltaTime;            // расстояние, которое пролетает пуля

        if (_direction.magnitude <= _distanceThisFrame)                 // длина вектора до цели <= расстояние до цели
        {
            HitTarget();                                             // пуля попала в цель и запуск метода попадания
            return;
        }
        transform.Translate(_direction.normalized * _distanceThisFrame, Space.World);                     //движение пули
        transform.LookAt(Target);                                    // разворот п оси Х в сторону цели

    }
    private void HitTarget()
    {
        GameObject _effectInstatiate = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(_effectInstatiate, 5f);

        if (_explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(Target);
        }

        Destroy(gameObject);                                           // уничтожение пули
    }

    private void Explode()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider _collider in _colliders)
        {
            if (_collider.tag == "Enemy")
            {
                Damage(_collider.transform);
            }
        }
    }

    private void Damage(Transform _enemy)
    {
        Enemy _enemyBullet = _enemy.GetComponent<Enemy>();

        if (_enemyBullet != null)
        {
            _enemyBullet.TakeDamage(_damage);
        }         
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
