using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgThemeAS;
    [SerializeField] private AudioSource buttonAS;
    [SerializeField] private AudioSource brickAS;

    [Header("Clips Ref")]
    [SerializeField] private AudioClip mainMenuBG;
    [SerializeField] private AudioClip gameplayBG;
    [SerializeField] private AudioClip gameOverBG;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip[] brickHitClips;

    [Range(0f, 1f)]
    [SerializeField] private float volumeControl;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 0.5f);
        }

        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        AudioListener.volume = volume;
    }

    private IEnumerator Start()
    {
        yield return null;

        if (GameManager.Instance == null)
        {
            Debug.LogError("Gamemanager is not ready");
        }

        GameManager.Instance.OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameStates.MainMenu);
    }

    private void Update()
    {
        AudioListener.volume = volumeControl;
    }

    private void OnGameStateChange(GameStates states)
    {
        Debug.Log("Sound Manager");

        switch (states)
        {
            case GameStates.MainMenu:
                bgThemeAS.clip = mainMenuBG;
                bgThemeAS.Play();
                Debug.Log($"<color=red>Playing audio clip main menu</color>");
                break;
            case GameStates.TimerMenu:
                break;
            case GameStates.Playing:
                bgThemeAS.clip = gameplayBG;
                bgThemeAS.Play();
                Debug.Log($"<color=red>Playing audio clip gameplay</color>");
                break;
            case GameStates.Pause:
                bgThemeAS.Pause();
                break;
            case GameStates.GameOver:
                bgThemeAS.clip = gameOverBG;
                bgThemeAS.Play();
                break;
        }
    }

    public void PlayBrickAudio()
    {
        int index = Random.Range(0, brickHitClips.Length);
        AudioClip clipToPlay = brickHitClips[index];

        brickAS.pitch = Random.Range(0.9f, 1.1f);
        //audioSource.clip = clipToPlay;
        buttonAS.Stop();
        brickAS.PlayOneShot(clipToPlay);
        brickAS.pitch = 1f;  // Reset

        Debug.Log($"<color=cyan> Playing Brick Audio {clipToPlay.name}</color>");
    }

    public void PlayButtonAudio()
    {
        brickAS.Stop();
        buttonAS.Stop();
        buttonAS.PlayOneShot(buttonClip);
        Debug.Log($"<color=yellow> Playing Button Audio </color>");
    }

    public void PlayAudio(AudioSource audioSource, AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChange;
    }
}
