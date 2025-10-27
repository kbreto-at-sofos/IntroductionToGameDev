using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
   protected override void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.IsTouchingLayers(wallLayerMask))
      {
         if (gameObject.activeSelf)
         {
            //return bullet to the pool
            ObjectPoolManager.ReturnObjectToPool(gameObject);
         }
      }
      else
      {
         DamageCollisionIfComponent<PlayerController>(collision);
      }
   }
}
