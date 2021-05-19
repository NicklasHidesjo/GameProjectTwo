using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
        //Fades out current music
        AudioManager.instance.StopSound(AudioManager.instance.gameObject, 0.99f); 

    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        AudioManager.instance.PlayMusic(levelToLoad, 2f);
    }
}
