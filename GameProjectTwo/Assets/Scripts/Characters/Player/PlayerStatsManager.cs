using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private BarController barControllerHealth;
    private BarController barControllerHunger;
    private EndLevelCheck endLevelCheck;
    private ActivateLairFinder activateLairFinder;
    
    [Header("Hunger Variables")]
    [SerializeField] int maxSatiation;
    [SerializeField] int currentSatiation;
    public int MaxSatiation { get => maxSatiation; }
    public int CurrentSatiation { get => currentSatiation; }
    
    [Header("Health Variables")]
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 100;
    public int CurrentHealth { get => currentHealth; }
    public int MaxHealth { get => maxHealth; }
    
    private bool isDead = false;
    public bool IsDead => isDead;

    void Start()
    {
        if (GameObject.Find("EndOfLevelTrigger") != null)
        {
            endLevelCheck = GameObject.FindWithTag("Lair").GetComponent<EndLevelCheck>();
        }
        else
        {
            Debug.LogWarningFormat("EndOfLevelTrigger not found in scene");
        }

        if (GetComponent<ActivateLairFinder>() != null)
        {
            activateLairFinder = GetComponent<ActivateLairFinder>();
        }
        else
        {
            Debug.LogWarningFormat("DirectionToLair not found");
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

    public void IncreaseHealthValue(int healthIncrease)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthIncrease, 0, maxHealth);
        SetCurrentBarValue(barControllerHealth, currentHealth);
    }

    public void DecreaseHealthValue(int healthDecrease)
    {
        currentHealth = Mathf.Clamp(currentHealth - healthDecrease, 0, maxHealth);
        SetCurrentBarValue(barControllerHealth, currentHealth);
        isDead = currentHealth <= 0;
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
        
        if (GameObject.FindWithTag("Lair") != null)
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
