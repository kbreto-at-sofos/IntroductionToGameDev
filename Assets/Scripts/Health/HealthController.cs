using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public UnityEvent onDied;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    public HealthBarUI healthBarUI;

    public bool IsInvincible { get; set; } = false;
    private InvincibilityController _invincibilityController;
    

    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();
    }

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

        if (IsInvincible)
        {
            return;
        }
        
        
        currentHealth -= damage;
        _invincibilityController?.StartInvincibility();
        healthBarUI.UpdateHealthBar(this);

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            onDied?.Invoke();
            // EventSubscriber<GameObject>.Publish(GameEvent.OnDied, gameObject);
        }
    }
}