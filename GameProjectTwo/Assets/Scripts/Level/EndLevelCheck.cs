using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCheck : MonoBehaviour
{
    private int currentLevel;
    public int CurrentLevel { get => currentLevel; }

    private PlayerStatsManager playerStatsManager;
	[SerializeField] MenuManager menuManager;

	public delegate void LevelEnd(int newLevel);
	public static event LevelEnd OnLevelEnded;

	public int[] LevelPassedThreshold { get => levelPassedThreshold; }

    [SerializeField] int[] levelPassedThreshold = new int[5];


    [SerializeField] LevelSettings levelSettings;


    private void Start()
    {
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
		menuManager = (MenuManager)FindObjectOfType(typeof(MenuManager));

		if (!levelSettings)
            levelSettings = GetComponent<LevelSettings>();
    }

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (CheckLevelPassed(playerStatsManager.CurrentSatiation))
			{
				if (currentLevel == levelPassedThreshold.Length - 1)
				{
					Debug.Log("Victory");
					menuManager.VictoryScreen();
				}
				else
				{
					Debug.Log("Level Completed");
					menuManager.EndOfLevelScreen();
					currentLevel++;
					playerStatsManager.ResetStats();
					//levelSettings.LevelStart();
					if (OnLevelEnded != null)
					{
						OnLevelEnded(currentLevel);
					}
					NPCSpawner.Instance.ResetNPCs();
					AudioManager.instance.PlaySound(SoundType.DraculaCoffin);
				}
				
			}
		}

    }
    private bool CheckLevelPassed(int satiation)
    {
        return satiation >= levelPassedThreshold[currentLevel];
    }
}
