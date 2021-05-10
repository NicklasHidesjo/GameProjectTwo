using UnityEngine;
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
