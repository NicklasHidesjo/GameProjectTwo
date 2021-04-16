using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerManager : MonoBehaviour
{
    private int hunger;

    public PlayerHungerMeter playerHungerMeter;
    public int Hunger { get { return hunger; } }

    private void Start()
    {
        playerHungerMeter = GameObject.FindGameObjectWithTag("HungerMeter").GetComponent<PlayerHungerMeter>();
        playerHungerMeter.SetMaxHungerValue(3);
        playerHungerMeter.ChangeHungerValue(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            hunger++;
            SetHunger(hunger);
            Debug.Log(Hunger);
        }
    }

    public void SetHunger(int newHunger)
    {
        hunger = newHunger;
        playerHungerMeter.ChangeHungerValue(hunger);
    }
}
