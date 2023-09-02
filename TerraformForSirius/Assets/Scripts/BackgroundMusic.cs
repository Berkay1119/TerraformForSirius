using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This object will not be destroyed when a new scene is loaded.
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate music objects to ensure only one is playing.
        }
    }
}
