using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletDelay = 0.5f;
    private float _lastFireTime;
    [SerializeField] private float bulletReleaseDelay = 10;

    private bool _fireContinuously;

    // Update is called once per frame
    void Update()
    {
        
        if (_fireContinuously)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= bulletDelay)
            {
                FireBullet();
                _lastFireTime = Time.time;
            }
        }
    }

    void FireBullet()
    {
        var bullet = ObjectPoolManager.SpawnObject(bulletPrefab, transform.position, Quaternion.identity);
        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        
        bulletRigidbody.velocity = bulletSpeed * PlayerStats.FacingDirection;

        StartCoroutine(ReleaseObjectAfterTime(bullet, bulletReleaseDelay));
    }

    private IEnumerator ReleaseObjectAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        if (obj.activeSelf)
        {
            ObjectPoolManager.ReturnObjectToPool(obj);
        }
    }

    private void OnFire(InputValue value)
    {
        _fireContinuously = value.isPressed;
    }
}