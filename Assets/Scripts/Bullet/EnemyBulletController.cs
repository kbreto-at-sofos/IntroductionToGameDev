using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
   protected override void OnTriggerEnter2D(Collider2D collision)
   {
      DamageCollisionIfComponent<PlayerController>(collision);
   }
}
