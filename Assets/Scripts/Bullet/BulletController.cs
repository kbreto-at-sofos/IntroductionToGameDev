using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] protected float damage = 30f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        ReleaseWhenOffScreen();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        DamageCollisionIfComponent<EnemyController>(collision);
    }

    protected void DamageCollisionIfComponent<T>(Collider2D collision)
    {
        // check if collision has wanted component 
        var component = collision.GetComponent<T>();
        if (component != null && gameObject.activeSelf)
        {
            //return bullet to the pool
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            
            // remove health if health controller
            var healthController = collision.GetComponent<HealthController>();
            if (healthController)
            {
                healthController.TakeDamage(damage);
            }
        }
    }

    private void ReleaseWhenOffScreen()
    {
        Vector2 screenPos = _camera.WorldToScreenPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > _camera.pixelWidth || screenPos.y < 0 || screenPos.y > _camera.pixelHeight)
        {
            if (gameObject.activeSelf)
            {
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }
        }
    }

    public static GameObject ShootBullet(GameObject bulletPrefab, Vector3 spawnPosition, Vector2 direction, float bulletSpeed)
    {
        var bullet = ObjectPoolManager.SpawnObject(bulletPrefab, spawnPosition, Quaternion.identity);
        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        
        bulletRigidbody.velocity = bulletSpeed * direction;

        return bullet;
    }
}
