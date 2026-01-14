using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Texts Ref")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text levelCompleteText;

    [Header("Menus Ref")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject timeSelectionMenu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject instructionMenu;
    [SerializeField] private GameObject pauseMenu;

    [Header("Buttons Ref")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button deleteDataButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button pauseButton;

    private const string HasSeenInstructionKey = "HasSeenInstruction";

    private void Start()
    {
        if (PlayerPrefs.HasKey(HasSeenInstructionKey))
        {
            instructionMenu.SetActive(false);
        }
        else
        {
            instructionMenu.SetActive(true);
        }
    }


    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        playButton.onClick.AddListener(OnPlayButton);
        replayButton.onClick.AddListener(OnReplayButton);
        deleteDataButton.onClick.AddListener(DeleteGameData);
        nextButton.onClick.AddListener(OnNextButton);
        pauseButton.onClick.AddListener(OnPause);

    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        playButton.onClick.RemoveListener(OnPlayButton);
        replayButton.onClick.RemoveListener(OnReplayButton);
        deleteDataButton.onClick.RemoveListener(DeleteGameData);
        nextButton.onClick.RemoveListener(OnNextButton);
        pauseButton.onClick.AddListener(OnPause);

        if (punchScaleCoroutine != null)
        {
            StopCoroutine(punchScaleCoroutine);
        }
    }

    private void OnGameStateChanged(GameStates newGameState)
    {
        DisableAllMenu();

        switch (newGameState)
        {
            case GameStates.MainMenu:
                startMenu.SetActive(true);
                break;
            case GameStates.TimerMenu:
                timeSelectionMenu.SetActive(true);
                break;
            case GameStates.Playing:
                inGameUI.SetActive(true);
                break;
            case GameStates.Pause:
                pauseMenu.SetActive(true);
                break;
            case GameStates.GameOver:
                timerText.text = FormatedTimer(0);
                scoreText.text = $"Score: 0";
                gameOverMenu.SetActive(true);
                break;
        }
    }

    private void DisableAllMenu()
    {
        startMenu.SetActive(false);
        timeSelectionMenu.SetActive(false);
        inGameUI.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }


    // ==Listeners==
    private void OnPlayButton()
    {
        SoundManager.Instance.PlayButtonAudio();
        GameManager.Instance.ChangeState(GameStates.TimerMenu);
    }

    private void OnReplayButton()
    {
        SoundManager.Instance.PlayButtonAudio();
        GameManager.Instance.ChangeState(GameStates.Playing);
    }
    private void DeleteGameData()
    {
        SoundManager.Instance.PlayButtonAudio();
        PlayerPrefs.DeleteAll();
    }
    private void OnPause()
    {
        SoundManager.Instance.PlayButtonAudio();
        GameManager.Instance.ChangeState(GameStates.Pause);
    }

    public void OnResumeButton()
    {
        SoundManager.Instance.PlayButtonAudio();
        Time.timeScale = 1;
        OnGameStateChanged(GameStates.Playing);
    }

    public void OnStartMenu()
    {
        SoundManager.Instance.PlayButtonAudio();
        StartCoroutine(DelayReloadScene());
    }

    private IEnumerator DelayReloadScene()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnNextButton()
    {
        SoundManager.Instance.PlayButtonAudio();
        instructionMenu.SetActive(false);
        PlayerPrefs.SetString(HasSeenInstructionKey, "true");
        PlayerPrefs.Save();
    }

    public void SetTimer(int seconds)
    {
        SoundManager.Instance.PlayButtonAudio();
        GameManager.Instance.SetGameplayTime(seconds);
        GameManager.Instance.ChangeState(GameStates.Playing);
    }

    public void UpdateCountdownUI(int seconds)
    {
        countdownText.gameObject.SetActive(true);

        if (seconds <= 0)
        {
            countdownText.text = "GO";
            StartCoroutine(DisableCountdown());
            return;
        }

        countdownText.text = seconds.ToString();
    }

    private IEnumerator DisableCountdown()
    {
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    private Coroutine punchScaleCoroutine;

    public void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {newScore}";
            //if (punchScaleCoroutine != null)
            //{
            //    StopCoroutine(punchScaleCoroutine);
            //}
            //punchScaleCoroutine = StartCoroutine(PunchScale(scoreText.transform));

            //Vector3 originalScale = scoreText.transform.localScale;
            //scoreText.gameObject.transform.DOPunchScale(originalScale * 0.2f, 0.1f)
            //    .OnComplete(() =>
            //    {
            //        scoreText.gameObject.transform.localScale = originalScale;
            //    });

            PopAnim(scoreText.gameObject.transform, 1.2f, 0.5f);
        }
    }

    public void UpdateTimerUI(int seconds, bool playAnim = false)
    {
        timerText.text = FormatedTimer(seconds);
        if(playAnim)
            PopAnim(timerText.gameObject.transform, 1.2f, 0.5f);
    }

    public void OnLevelComplete()
    {
        levelCompleteText.gameObject.SetActive(true);

        Vector3 originalScale = levelCompleteText.transform.localScale;


        PopAnim(levelCompleteText.gameObject.transform, 1.5f, 1f, true);
    }

    public void UpdateHighscoreText(int score)
    {
        highScoreText.text = $"High Score: {score}";
    }

    private string FormatedTimer(int seconds)
    {
        int mins = seconds / 60;
        int secs = seconds % 60;

        return $"Timer: {mins}:{secs}";
    }

    private void PopAnim(Transform target, float scaleTargetVal, float duration, bool shouldDeactivate = false)
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 originalScale = target.localScale;

        sequence.Append(target.DOScale(scaleTargetVal, duration).SetEase(Ease.OutBounce));
        sequence.Append(target.DOScale(originalScale, 0.5f).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                if (shouldDeactivate)
                    target.gameObject.SetActive(false);
            }));

        sequence.Play();
    }

    //private IEnumerator PunchScale(Transform target)
    //{
    //    Vector3 originalScale = Vector3.one;

    //    float elapsed = 0f;
    //    float duration = 0.15f;
    //    Vector3 scale = originalScale * 1.3f;
    //    target.localScale = scale;

    //    while (elapsed < duration)
    //    {
    //        float x = scale.x - Time.unscaledDeltaTime;
    //        float y = scale.y - Time.unscaledDeltaTime;

    //        scale = new Vector3(x, y, 0);

    //        if (scale.x > 1f && scale.y > 1f)
    //        {
    //            target.localScale = scale;
    //        }

    //        elapsed += Time.unscaledDeltaTime;
    //        yield return null;
    //    }

    //    target.localScale = Vector3.one;
    //}
}
