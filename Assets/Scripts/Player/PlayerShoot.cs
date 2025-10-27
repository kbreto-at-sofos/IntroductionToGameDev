using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletDelay = 0.5f;
    
    private float _lastFireTime;
    private bool _fireContinuously;
    private bool _fireSingle;

    // Update is called once per frame
    void Update()
    {
        
        if (_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= bulletDelay)
            {
                FireBullet();
                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }
    }

    void FireBullet()
    {
        BulletController.ShootBullet(bulletPrefab, transform.position, PlayerStats.FacingDirection, bulletSpeed);
    }

    private void OnFire(InputValue value)
    {
        _fireContinuously = value.isPressed;

        if (value.isPressed)
        {
            _fireSingle = true;
        }
    }
}