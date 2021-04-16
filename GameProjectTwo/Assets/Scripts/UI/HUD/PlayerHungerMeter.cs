using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHungerMeter : MonoBehaviour
{
	public Slider hungerMeterFill;

	public void SetMaxHungerValue(int hunger)
	{
		hungerMeterFill.maxValue = hunger;
	}

	public void ChangeHungerValue(int hunger)
	{
		hungerMeterFill.value = hunger;
	}
}
