using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsManager))]
public class HealthManager : MonoBehaviour
{
    private StatsManager statsManager;

    private void Start()
    {
        statsManager = GetComponent<StatsManager>();
    }

    public void LoseHealth(int health)
    {
        statsManager.DecreaseHealthValue(health);
    }

    public void GainHealth(int health)
    {
        statsManager.IncreaseHealthValue(health);
    }
    
}
