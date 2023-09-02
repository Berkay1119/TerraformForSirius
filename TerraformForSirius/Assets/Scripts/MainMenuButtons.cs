using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    public void OnPlayClicked()
    {
        // Load the next scene based on the current active scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        SoundManager.instance.PlayButtonClickSound();
    }

    public void OnSettingsClicked()
    {
        settingsPanel.SetActive(true);
        SoundManager.instance.PlayButtonClickSound();
    }

    public void OnCreditsClicked()
    {
        creditsPanel.SetActive(true);
        SoundManager.instance.PlayButtonClickSound();
    }

    public void OnQuitClicked()
    {
        // Quit the game (This will only work after building the game. It won't quit the play mode in the Unity Editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnCloseSettingsClicked()
{
    settingsPanel.SetActive(false);
    SoundManager.instance.PlayButtonClickSound();
}

public void OnCloseCreditsClicked()
{
    creditsPanel.SetActive(false);
    SoundManager.instance.PlayButtonClickSound();
}
}
