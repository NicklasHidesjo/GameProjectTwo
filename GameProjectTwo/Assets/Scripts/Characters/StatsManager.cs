using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatsManager : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] protected int currentHealth = 0;
    [SerializeField] protected int maxHealth = 100;
    public int CurrentHealth { get => currentHealth; }
    public int MaxHealth { get => maxHealth; }
    
    protected bool isDead = false;

    public bool IsDead { get => isDead; }

    public virtual void IncreaseHealthValue(int healthIncrease)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthIncrease, 0, maxHealth);
    }

    public virtual void DecreaseHealthValue(int healthDecrease)
    {
        currentHealth = Mathf.Clamp(currentHealth - healthDecrease, 0, maxHealth);
        isDead = currentHealth <= 0;
    }
}
