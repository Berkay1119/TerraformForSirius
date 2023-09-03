using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject infoPanel;         // The panel containing the images
    [SerializeField] private Image displayImage;           // The Image component to show the sprites
    [SerializeField] private Button nextButton;            // Button to navigate to the next image
    [SerializeField] private Button prevButton;            // Button to navigate to the previous image
    [SerializeField] private Button closeButton;           // Button to close the panel

    [Header("Sprites")]
    [SerializeField] private Sprite[] infoSprites;         // Array of sprites to display in order

    private int currentSpriteIndex = 0;                    // Index to track the current displayed sprite

    private void Start()
    {
        // Setup button listeners
        nextButton.onClick.AddListener(ShowNextImage);
        prevButton.onClick.AddListener(ShowPrevImage);
        closeButton.onClick.AddListener(ClosePanel);

        // Initially set the first image (if available)
        if (infoSprites.Length > 0)
            displayImage.sprite = infoSprites[currentSpriteIndex];
    }

    // Navigate to the next image in the array
    private void ShowNextImage()
    {
        if (currentSpriteIndex < infoSprites.Length - 1)
        {
            currentSpriteIndex++;
            displayImage.sprite = infoSprites[currentSpriteIndex];
        }
        UpdateButtonInteractivity();
    }

    // Navigate to the previous image in the array
    private void ShowPrevImage()
    {
        if (currentSpriteIndex > 0)
        {
            currentSpriteIndex--;
            displayImage.sprite = infoSprites[currentSpriteIndex];
        }
        UpdateButtonInteractivity();
    }

    // Close the information panel
    private void ClosePanel()
    {
        infoPanel.SetActive(false);
    }

    // Update button interactivity based on the current displayed sprite
    private void UpdateButtonInteractivity()
    {
        prevButton.interactable = currentSpriteIndex > 0;
        nextButton.interactable = currentSpriteIndex < infoSprites.Length - 1;
    }

    public void ShowPanel()
{
    infoPanel.SetActive(true);
    UpdateButtonInteractivity();  // Make sure the navigation buttons are correctly set up
    currentSpriteIndex = 0;       // Reset to the first image (optional)
    displayImage.sprite = infoSprites[currentSpriteIndex];  // Display the first image (optional)
}
}
