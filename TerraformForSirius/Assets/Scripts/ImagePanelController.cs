using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImagePanelController : MonoBehaviour
{
    public Sprite[] sprites;
    private int currentSpriteIndex = 0;
    public Image displayImage;
    public GameObject panel; // This is your panel which will be set active.
    public Button closeButton;
    public Button nextButton;

    private void Start()
    {
        // Assuming the close button is a child of the panel, if not, set it manually in the inspector
        closeButton.onClick.AddListener(ClosePanel);
        
        // Assuming the next button is a child of the panel, if not, set it manually in the inspector
        nextButton.onClick.AddListener(NextSprite);
        
        // Assuming the image is a child of the panel, if not, set it manually in the inspector
        displayImage.sprite = sprites[currentSpriteIndex];
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) // When the scene with index 1 is loaded
        {
            panel.SetActive(true);
        }
    }

    public void NextSprite()
    {
        currentSpriteIndex++;
        if (currentSpriteIndex >= sprites.Length)
        {
            currentSpriteIndex = 0;
        }
        displayImage.sprite = sprites[currentSpriteIndex];
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
