using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private HealthManager healthManager;
    private BarController barController;

    private bool isDead = false;
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        barController = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<BarController>();
        barController.SetMaxBarValue(healthManager.MaxHealth);
        barController.SetCurrentBarValue(healthManager.CurrentHealth);
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
            barController.SetCurrentBarValue(healthManager.CurrentHealth);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            healthManager.GainHealth(5);
            barController.SetCurrentBarValue(healthManager.CurrentHealth);
        }
    }
}
