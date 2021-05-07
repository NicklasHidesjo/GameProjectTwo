using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("End of level Screen")]
    [SerializeField] private GameObject endOfLevelScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject victoryScreen;

    private bool inDeathScreen;

    private bool gamePaused = false;

    public delegate void LevelStart();
    public static event LevelStart OnLevelStart;

    private void Start()
    {
        inDeathScreen = false;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                pauseScreen.SetActive(false);
                TogglePause();
            }
            else
            {
                pauseScreen.SetActive(true); 
                TogglePause();                
            }
        }

        if (PlayerManager.instance.StatsManager.IsDead && !inDeathScreen)
        {
            PlayerDeathScreen();
        }
    }

    public void RestartGame()
    {
        TogglePause();
        //TODO Load correct scene
        String currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
        AudioManager.instance.StopAll2DSounds();
    }

    public void TogglePause()
    {
        if (gamePaused)
        {
            PlayerManager.instance.PlayerState.SetState(PlayerState.playerStates.DraculaDefault);
            Cursor.visible = false;
            Time.timeScale = 1f;
            gamePaused = false;
        }
        else
        {
            PlayerManager.instance.PlayerState.SetState(PlayerState.playerStates.Stoped);
            Cursor.visible = true;
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }

    public void StartNextLevel()
    {
        if (OnLevelStart != null)
        {
            OnLevelStart();
        }
    }

    private void PlayerDeathScreen()
    {
        inDeathScreen = true;
        deathScreen.SetActive(true);
        AudioManager.instance.StopAll2DSounds();
        TogglePause();
    }

    public void EndOfLevelScreen()
    {
        
        endOfLevelScreen.SetActive(true);
        AudioManager.instance.StopAll2DSounds();

        TogglePause();
    }

    public void VictoryScreen()
    {
        victoryScreen.SetActive(true);
        TogglePause();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
