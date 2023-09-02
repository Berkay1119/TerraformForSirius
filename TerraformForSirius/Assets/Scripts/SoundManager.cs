using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip buttonClickSound;

    public AudioClip HexPlacement;
    // Add more AudioClips for different sound effects as needed

    public AudioClip HexChoose;

    private AudioSource audioSource;

    private void OnEnable() {
        EventManager.SoundEffect+= PlaySound;
    }

    private void OnDisable() {
        EventManager.SoundEffect -= PlaySound;
    }


    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(SoundEffectTypes soundEffect)
    {
        if (soundEffect==SoundEffectTypes.HexPlacement)
        {
            PlaySound(HexPlacement);
        }
        else if (soundEffect==SoundEffectTypes.HexChoose)
        {
            PlaySound(HexChoose);
        }
        
    }

    public void PlayButtonClickSound()
    {
        PlaySound(buttonClickSound);
    }

    // You can add more functions for different sound effects

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
