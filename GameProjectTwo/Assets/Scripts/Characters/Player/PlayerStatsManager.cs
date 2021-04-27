using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    private BarController barControllerHealth;
    private BarController barControllerStamina;
    private EndLevelCheck endLevelCheck;
    private Text satiationText;
    
    [Header("Hunger Variables")]
    [SerializeField] int maxSatiation;
    [SerializeField] int currentSatiation;
    public int MaxSatiation { get => maxSatiation; }
    public int CurrentSatiation { get => currentSatiation; }

    private String maxSatiationNumber = "";
    private String currentSatiationNumber = "";
    
    [Header("Stamina Variables")]
    [SerializeField] float maxStamina = 100;
    [SerializeField] float currentStamina = 100;
    [SerializeField] private float staminaRecoveryRate = 10f;
    public float MaxStamina { get => maxStamina; }
    public float CurrentStamina { get => currentStamina; }
    
    [Header("Health Variables")]
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 100;
    [SerializeField] GameObject Lairfinder;
    public int CurrentHealth { get => currentHealth; }
    public int MaxHealth { get => maxHealth; }
    
    private bool isDead = false;
    public bool IsDead => isDead;

    void Start()
    {
        if (GameObject.FindWithTag("Lair") != null)
        {
            endLevelCheck = GameObject.FindWithTag("Lair").GetComponent<EndLevelCheck>();
        }
        else
        {
            Debug.LogWarningFormat("EndOfLevelTrigger not found in scene");
        }

        satiationText = GameObject.FindGameObjectWithTag("HungerMeter").GetComponent<Text>();
        
        barControllerHealth = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<BarController>();
        barControllerStamina = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<BarController>();
        barControllerHealth.Init();
        barControllerStamina.Init();
        ResetStats();
    }
    
    void Update()
    {
        //Passive stamina recovery
        if (currentStamina < maxStamina)
        {
            IncreaseStaminaValue(staminaRecoveryRate * Time.deltaTime);
        }

        //Everything below here in Update() is for testing purposes
        if (Input.GetKeyDown(KeyCode.T))
        {
            DecreaseStaminaValue(20);
        }
    }

    private void SetCurrentBarValue(BarController barController, float value)
    {
        barController.SetCurrentValue(value);
    }

    private void SetMaxBarValue(BarController barController, float maxValue)
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
        
        //Only for debug, to be removed later
        if (isDead)
        {
            Debug.Log("You lost. Git gud scrub");
        }
    }

    public void IncreaseSatiationValue(int satiationIncrease)
    {
        currentSatiation = Mathf.Clamp(currentSatiation + satiationIncrease, 0, maxSatiation);

        currentSatiationNumber = currentSatiation.ToString();
        satiationText.text = currentSatiationNumber + " / " + maxSatiationNumber;
        
        if (currentSatiation >= maxSatiation)
        {
            Debug.Log("LairFinder Active");
            Lairfinder.SetActive(true);
        }
    }

    public void IncreaseStaminaValue(float staminaIncrease)
    {
        currentStamina = Mathf.Clamp(currentStamina + staminaIncrease, 0, maxStamina);
        SetCurrentBarValue(barControllerStamina, currentStamina);
    }

    public void DecreaseStaminaValue(float staminaDecrease)
    {
        currentStamina = Mathf.Clamp(currentStamina - staminaDecrease, 0, maxStamina);
        SetCurrentBarValue(barControllerStamina, currentStamina);
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
 
        Lairfinder.SetActive(false);

        currentSatiationNumber = currentSatiation.ToString();
        maxSatiationNumber = maxSatiation.ToString();

        SetMaxBarValue(barControllerStamina, maxStamina);
        SetMaxBarValue(barControllerHealth, maxHealth);
        SetCurrentBarValue(barControllerStamina, currentStamina);
        SetCurrentBarValue(barControllerHealth, currentHealth);
        satiationText.text = currentSatiationNumber + " / " + maxSatiationNumber;
    }
    
}
