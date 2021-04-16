using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private HealthManager healthManager;
    private PlayerHealthBar playerHealthBar;

    private bool isDead = false;
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        playerHealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<PlayerHealthBar>();
        playerHealthBar.SetMaxHealthBarValue(healthManager.MaxHealth);
        playerHealthBar.ChangeHealthBarValue(healthManager.CurrentHealth);
    }
    
    void Update()
    {
        if (isDead)
        {
            Debug.Log("You lost. Git gud scrub");
            isDead = false;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            isDead = healthManager.LoseHealth(5);
            playerHealthBar.ChangeHealthBarValue(healthManager.CurrentHealth);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            healthManager.GainHealth(5);
            playerHealthBar.ChangeHealthBarValue(healthManager.CurrentHealth);
        }
    }
}
