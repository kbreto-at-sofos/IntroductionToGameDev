using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyController = collision.GetComponent<EnemyController>();
        if (enemyController && gameObject.activeSelf)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            Destroy(enemyController.gameObject);
        }
    }
}
