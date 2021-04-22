using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCheck : MonoBehaviour
{
	private int currentLevel;

	public int CurrentLevel { get => currentLevel; }

	private PlayerStatsManager playerStatsManager;

	public int[] LevelPassedThreshold { get => levelPassedThreshold; }

	[SerializeField] int[] levelPassedThreshold = new int [5];

    private void Start()
    {
		playerStatsManager = PlayerManager.instance.gameObject.GetComponent<PlayerStatsManager>();
	}

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//playerStatsManager = other.GetComponent<PlayerStatsManager>();
			

			if (CheckLevelPassed(playerStatsManager.CurrentSatiation))
			{
				Debug.Log("Level Completed");
				currentLevel++;
				playerStatsManager.ResetStats();
			}
		}
	}
	private bool CheckLevelPassed(int satiation)
	{
		return satiation >= levelPassedThreshold[currentLevel];
	}
}
