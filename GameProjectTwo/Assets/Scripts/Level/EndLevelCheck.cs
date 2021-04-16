using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCheck : MonoBehaviour
{
	private int currentLevel;

	public int CurrentLevel { get => currentLevel; }

	public int[] LevelPassedThreshold { get => levelPassedThreshold; }

	private bool levelPassed = false;

	[SerializeField] int[] levelPassedThreshold = new int [5];

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerStatsManager>() != null)
		{
			CheckLevelPassed(other.GetComponent<HungerManager>().Hunger);
			if (levelPassed)
			{
				other.GetComponent<HungerManager>().SetHunger(0);
				other.GetComponent<HungerManager>().playerHungerMeter.
					SetMaxHungerValue(levelPassedThreshold[currentLevel]);
				levelPassed = false;
			}
		}
	}
	private void CheckLevelPassed(int hunger)
	{
		if (hunger >= levelPassedThreshold[currentLevel])
		{
			Debug.Log("Level Completed");
			currentLevel++;
			levelPassed = true;
			//EndOfLevelScreen();
		}
	}
}
