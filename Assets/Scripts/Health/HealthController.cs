using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;

    public float RemainingHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {

        if (currentHealth <= 0)
        {
            return;
        }
        
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
            EventSubscriber<GameObject>.Publish(GameEvent.OnDied, gameObject);
        }
    }
}