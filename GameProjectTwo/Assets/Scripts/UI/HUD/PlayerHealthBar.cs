using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
	public Slider healthBarFill;

	public void SetMaxHealthBarValue(int health)
	{
		healthBarFill.maxValue = health;
	}
	
	public void ChangeHealthBarValue(int health)
	{
		healthBarFill.value = health;
	}
}
