using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerManager : MonoBehaviour
{
    private int hunger;

    public BarController barController;
    public int Hunger { get { return hunger; } }

    private void Start()
    {
        barController = GameObject.FindGameObjectWithTag("HungerMeter").GetComponent<BarController>();
        barController.SetMaxBarValue(3);
        barController.SetCurrentBarValue(0);
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
        barController.SetCurrentBarValue(hunger);
    }
}
