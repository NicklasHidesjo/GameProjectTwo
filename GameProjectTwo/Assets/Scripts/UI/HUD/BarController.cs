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

	public void SetMaxBarValue(int hunger)
	{
		barFill.maxValue = hunger;
	}

	public void SetCurrentBarValue(int hunger)
	{
		barFill.value = hunger;
	}
}
