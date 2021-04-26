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
	}

    public void Init()
    {
		barFill = GetComponent<Slider>();
    }

    public void SetMaxValue(float maxValue)
	{
		barFill.maxValue = maxValue;
	}

	public void SetCurrentValue(float currentValue)
	{
		barFill.value = currentValue;
	}
}
