using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : StatsManager
{
    private BarController barControllerHealth;
    private BarController barControllerHunger;
    private EndLevelCheck endLevelCheck;
    private ActivateLairFinder activateLairFinder;
    
    private int maxSatiation;
    private int currentSatiation;
    public int MaxSatiation { get => maxSatiation; }
    public int CurrentSatiation { get => currentSatiation; }

    void Start()
    {
        if (GameObject.Find("EndOfLevelTrigger") != null)
        {
            endLevelCheck = GameObject.Find("EndOfLevelTrigger").GetComponent<EndLevelCheck>();
        }
        else
        {
            Debug.LogWarningFormat("EndOfLevelTrigger not found in scene");
        }

        if (GetComponent<DirectionToLair>() != null)
        {
            activateLairFinder = GetComponent<ActivateLairFinder>();
        }
        else
        {
            Debug.LogWarningFormat("Lair not found");
        }
        
        barControllerHealth = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<BarController>();
        barControllerHunger = GameObject.FindGameObjectWithTag("HungerMeter").GetComponent<BarController>();

        ResetStats();
    }
    
    void Update()
    {
        //All current IF-statements are for testing purposes.
        if (isDead)
        {
            Debug.Log("You lost. Git gud scrub");
            isDead = false;
        }
    }

    private void SetCurrentBarValue(BarController barController, int value)
    {
        barController.SetCurrentValue(value);
    }

    private void SetMaxBarValue(BarController barController, int maxValue)
    {
        barController.SetMaxValue(maxValue);
    }

    public override void IncreaseHealthValue(int healthIncrease)
    {
        base.IncreaseHealthValue(healthIncrease);
        SetCurrentBarValue(barControllerHealth, currentHealth);
    }

    public override void DecreaseHealthValue(int healthDecrease)
    {
        base.DecreaseHealthValue(healthDecrease);
        SetCurrentBarValue(barControllerHealth, currentHealth);
    }

    public void IncreaseSatiationValue(int satiationIncrease)
    {
        currentSatiation = Mathf.Clamp(currentSatiation + satiationIncrease, 0, maxSatiation);
        SetCurrentBarValue(barControllerHunger, currentSatiation);

        if (currentSatiation >= maxSatiation && activateLairFinder != null)
        {
            Debug.Log("LairFinder Active");
            activateLairFinder.Activate();
        }
    }

    public void ResetStats()
    {
        currentSatiation = 0;
        
        if (GameObject.Find("EndOfLevelTrigger") != null)
        {
            maxSatiation = endLevelCheck.LevelPassedThreshold[endLevelCheck.CurrentLevel];
        }
        else
        {
            maxSatiation = 5;
        }
        currentHealth = maxHealth;
        
        if (activateLairFinder != null)
        {
            activateLairFinder.Deactivate();
        }

        SetCurrentBarValue(barControllerHunger, currentSatiation);
        SetMaxBarValue(barControllerHunger, maxSatiation);
        SetCurrentBarValue(barControllerHealth, currentHealth);
        SetMaxBarValue(barControllerHealth, maxHealth);
    }
    
}
