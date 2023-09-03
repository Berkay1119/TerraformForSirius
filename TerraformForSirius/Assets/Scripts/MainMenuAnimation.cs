using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public GameObject[] smokes;
    public float moveSpeed = 5f;

    public GameObject rotatingSprite;
    public float rotationSpeed = 50f;

    public GameObject[] buttons;
    private Vector2[] originalButtonPositions;
    public float buttonOscillationSpeed = 1f;
    public float buttonOscillationHeight = 0.5f;

    private bool isAnimating = true;

    private void Start()
    {
        // Store original button positions and set their scale to zero.
        originalButtonPositions = new Vector2[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalButtonPositions[i] = buttons[i].transform.position;
            buttons[i].transform.localScale = Vector2.zero;
        }

        StartAnimation();
        Invoke("StopAnimation", 3f);
        Invoke("ScaleButtonsToOriginal", 2f);
    }

    private void Update()
    {
        if (isAnimating)
        {
            foreach (GameObject smoke in smokes)
            {
                smoke.transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
        }

        rotatingSprite.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        OscillateButtons();
    }

    void StartAnimation()
    {
        isAnimating = true;
    }

    void StopAnimation()
    {
        isAnimating = false;
    }

    void ScaleButtonsToOriginal()
    {
        foreach (GameObject button in buttons)
        {
            button.transform.localScale = Vector2.one;  // Set scale to original.
        }
    }

    void OscillateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            float newY = originalButtonPositions[i].y + Mathf.Sin(Time.time * buttonOscillationSpeed) * buttonOscillationHeight;
            buttons[i].transform.position = new Vector2(originalButtonPositions[i].x, newY);
        }
    }
}
    