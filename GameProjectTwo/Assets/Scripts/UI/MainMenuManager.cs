using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
	[SerializeField] GameObject baseMenuPanel;
	[SerializeField] GameObject howToPlayPanel;
	[SerializeField] GameObject optionsPanel;

	
	private void Start()
	{
		baseMenuPanel.SetActive(true);
		howToPlayPanel.SetActive(false);
		optionsPanel.SetActive(false);
	}

	public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void ToggleHowToPlay()
	{
		howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
	}

	public void ToggleOptions()
	{
		optionsPanel.SetActive(!optionsPanel.activeSelf);
	}

	public void ToggleBaseMenu()
	{
		baseMenuPanel.SetActive(!baseMenuPanel.activeSelf);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
