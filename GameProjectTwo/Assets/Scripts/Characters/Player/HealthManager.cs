using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatsManager))]
public class HealthManager : MonoBehaviour
{
    private PlayerStatsManager playerStatsManager;

    private void Start()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    public void LoseHealth(int health)
    {
        playerStatsManager.DecreaseHealthValue(health);
    }

    public void GainHealth(int health)
    {
        playerStatsManager.IncreaseHealthValue(health);
    }
    
}
