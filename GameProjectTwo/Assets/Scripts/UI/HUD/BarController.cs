using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
	private Slider barFill;

	private void Start()
	{
		barFill = GetComponent<Slider>();
	}

	public void SetMaxValue(int maxValue)
	{
		barFill.maxValue = maxValue;
	}

	public void SetCurrentValue(int currentValue)
	{
		barFill.value = currentValue;
	}
}
