using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
   protected override void OnTriggerEnter2D(Collider2D collision)
   {
      var playerController = collision.GetComponent<PlayerController>();
      if (playerController && gameObject.activeSelf)
      {
         ObjectPoolManager.ReturnObjectToPool(gameObject);
         Debug.Log("player take damage");
      }
   }
}
