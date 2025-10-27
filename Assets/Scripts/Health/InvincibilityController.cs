using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{

    private HealthController _healthController;
    [SerializeField] private float invincibilityTime = 1f;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    public void StartInvincibility()
    {
        StartCoroutine(InvincibilityTimer(invincibilityTime));
    }

    private IEnumerator InvincibilityTimer(float waitTime)
    {
        _healthController.IsInvincible = true;
        yield return new WaitForSeconds(waitTime);
        _healthController.IsInvincible = false;
    }
}