using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int currentHealth = 0;
    public int CurrentHealth { get => currentHealth; }
    
    [SerializeField] int maxHealth = 100;
    public int MaxHealth { get => maxHealth; }

    void Start()
    {
        SetHealth(maxHealth);
    }

    private void SetHealth(int health)
    {
        currentHealth = health;
    }

    public bool LoseHealth(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth - health, 0, maxHealth);
        return currentHealth <= 0;
    }

    public void GainHealth(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
    }
    
    
}
