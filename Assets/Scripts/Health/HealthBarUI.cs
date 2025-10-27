using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    [SerializeField] private Image healthBarForeground;



    public void UpdateHealthBar(HealthController healthController)
    {
        healthBarForeground.fillAmount = healthController.RemainingHealthPercentage();
    }
}
