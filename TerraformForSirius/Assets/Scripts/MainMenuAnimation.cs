using UnityEngine;
using UnityEngine.UI; // For accessing Button component
using UnityEngine.EventSystems; // For IPointerEnterHandler and IPointerExitHandler

public class MainMenuAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] smokes;
    public float moveSpeed = 5f;

    public GameObject rotatingSprite;
    public float rotationSpeed = 50f;

    public GameObject[] buttons;
    private Vector2[] originalButtonPositions;
    public float buttonOscillationSpeed = 1f;
    public float buttonOscillationHeight = 0.5f;
    public float buttonHoverScale = 1.2f; // Set the scaling amount on hover

    private bool isAnimating = true;

    private void Start()
    {
        // Store original button positions and set their scale to zero.
        originalButtonPositions = new Vector2[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalButtonPositions[i] = buttons[i].transform.position;
            buttons[i].transform.localScale = Vector2.zero;

            // Add EventTrigger components for hover effect
            Button btn = buttons[i].GetComponent<Button>();
            EventTrigger trigger = buttons[i].GetComponent<EventTrigger>() ?? buttons[i].AddComponent<EventTrigger>();
            
            // Add PointerEnter event
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => { btn.transform.localScale = Vector2.one * buttonHoverScale; });
            trigger.triggers.Add(entryEnter);

            // Add PointerExit event
            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => { btn.transform.localScale = Vector2.one; });
            trigger.triggers.Add(entryExit);
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
            StartCoroutine(ScaleButton(button));
        }
    }

    System.Collections.IEnumerator ScaleButton(GameObject button)
    {
        float duration = 2f; // 2 seconds duration
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float scaleValue = Mathf.Lerp(0, 1, elapsed / duration);
            button.transform.localScale = new Vector2(scaleValue, scaleValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        button.transform.localScale = Vector2.one;
    }

    void OscillateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            float newY = originalButtonPositions[i].y + Mathf.Sin(Time.time * buttonOscillationSpeed) * buttonOscillationHeight;
            buttons[i].transform.position = new Vector2(originalButtonPositions[i].x, newY);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector2.one * buttonHoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector2.one;
    }
}