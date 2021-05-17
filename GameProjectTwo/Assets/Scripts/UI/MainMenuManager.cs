using UnityEngine;
public class MainMenuManager : MonoBehaviour
{
	[SerializeField] GameObject baseMenuPanel;
	[SerializeField] GameObject howToPlayPanel;
	[SerializeField] GameObject optionsPanel;
	[SerializeField] AudioOptions audioOptions;
	
	private void Start()
	{
		baseMenuPanel.SetActive(true);
		howToPlayPanel.SetActive(false);
		optionsPanel.SetActive(false);
		audioOptions = GetComponent<AudioOptions>();
	}

	public void ToggleHowToPlay()
	{
		howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
	}

	public void ToggleOptions()
	{
		optionsPanel.SetActive(!optionsPanel.activeSelf);
        if (optionsPanel.activeSelf)
        {
			audioOptions.SetSliders();

        }
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
