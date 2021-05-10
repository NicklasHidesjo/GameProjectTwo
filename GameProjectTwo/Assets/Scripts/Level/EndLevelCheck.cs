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
        playerStatsManager = PlayerManager.instance.gameObject.GetComponent<PlayerStatsManager>();
		menuManager = (MenuManager)FindObjectOfType(typeof(MenuManager));

		if (!levelSettings)
            levelSettings = GetComponent<LevelSettings>();
    }

    private void OnTriggerEnter(Collider other)
	{
		Debug.Log($"lv :  { currentLevel}");
		Debug.Log("37 : " + other.CompareTag("Player"));
		if (other.CompareTag("Player"))
		{
		Debug.Log("40 : " + playerStatsManager.CurrentSatiation);
			if (CheckLevelPassed(playerStatsManager.CurrentSatiation))
			{
		Debug.Log("43A : " + (levelPassedThreshold.Length - 1));
		Debug.Log($"43B : {currentLevel}");
				if (currentLevel == levelPassedThreshold.Length - 1)
				{
					Debug.Log("Victory");
					menuManager.VictoryScreen();
				}
				else
				{
		Debug.Log($"53 : ");
					Debug.Log("Level Completed");
					menuManager.EndOfLevelScreen();
					currentLevel++;
		Debug.Log($"57 : ");
					playerStatsManager.ResetStats();
					//levelSettings.LevelStart();
					if (OnLevelEnded != null)
					{
						OnLevelEnded(currentLevel);
					}
		Debug.Log($"64 : ");
					NPCSpawner.Instance.ResetNPCs();
		Debug.Log($"66 : ");
				}
				
			}
		}

    }
    private bool CheckLevelPassed(int satiation)
    {
        return satiation >= levelPassedThreshold[currentLevel];
    }
}
