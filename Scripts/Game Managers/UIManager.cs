using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.Rendering.FilterWindow;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _screenDimming;
    [SerializeField] private GameObject _cardSelectionUI;
    [SerializeField] private GameObject _menuGameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _finishGameUI;
    [SerializeField] private GameObject _audioUI;
    [SerializeField] private GameObject _pauseGameUI;
    [SerializeField] private GameObject _HPBarUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _HPBarFilling;
    [SerializeField] private Button _pauseButton;
    PlayerStatistics _playerStatistics;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _playerStatistics = PlayerController.Instance.PlayerStatistics;

        HideHUD();
        ShowMenuGameUI();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += ShowHUD;
        GameManager.Instance.OnFinishGame += HideHUD;
        GameManager.Instance.OnGameOver += GameOverUI;
        GameManager.Instance.OnGamePaused += ShowPauseGameUI;
        GameManager.Instance.OnGameContinued += HidePauseGameUI;
        PlayerController.Instance.PlayerStatistics.PlayerScore.OnScoreChanged += UpdateScoreUI;
        PlayerController.Instance.HealthPoints.OnHPChanged += UpdateHPBar;
        CardManager.Instance.OnStartCardSelection += StartCardSelectionUI;
        CardManager.Instance.OnFinishCardSelection += FinishCardSelectionUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= ShowHUD;
        GameManager.Instance.OnFinishGame -= HideHUD;
        GameManager.Instance.OnGameOver -= GameOverUI;
        GameManager.Instance.OnGamePaused -= ShowPauseGameUI;
        GameManager.Instance.OnGameContinued -= HidePauseGameUI;
        PlayerController.Instance.PlayerStatistics.PlayerScore.OnScoreChanged -= UpdateScoreUI;
        PlayerController.Instance.HealthPoints.OnHPChanged -= UpdateHPBar;
        CardManager.Instance.OnStartCardSelection -= StartCardSelectionUI;
        CardManager.Instance.OnFinishCardSelection -= FinishCardSelectionUI;
    }

    public void ShowMenuGameUI()
    {
        ShowAudioSlider();
        _menuGameUI.SetActive(true);
    }

    public void HideMenuGameUI()
    {
        HideAudioSlider();
        _menuGameUI.SetActive(false);
    }

    public void ShowPauseGameUI()
    {
        HidePauseButton();
        ScreenDimmingOn();
        ShowAudioSlider();
        _pauseGameUI.SetActive(true);
    }
    public void HidePauseGameUI()
    {
        ShowPauseButton();
        ScreenDimmingOff();
        HideAudioSlider();
        _pauseGameUI.SetActive(false);
    }

    private void GameOverUI()
    {
        HideHUD();
        ScreenDimmingOn();
        SetUIText(_gameOverUI.transform, "FinalScoreText",
            $"SCORE:\n{_playerStatistics.PlayerScore.GetCurrentScore():D8}");
        ShowGameOverUI();
    }

    public void FinishGameUI()
    {
        HideHUD();
        var parent = _finishGameUI.transform;

        SetUIText(parent, "FinalScoreText",
            $"SCORE:\n{_playerStatistics.PlayerScore.GetCurrentScore():D8}");

        SetUIText(parent, "ObstaclesDownText",
            $"OBSTACLES DOWN:\n{_playerStatistics.ObstaclesDown:D8}");

        SetUIText(parent, "Distance TraveledText",
            $"DISTANCE TRAVELED:\n{_playerStatistics.DistanceTraveled:D8}");

        ShowFinishGameUI();
    }

    private void ShowHUD()
    {
        ShowHPBar();
        ShowScore();
        ShowPauseButton();
    }

    public void HideHUD()
    {
        HideHPBar();
        HideScore();
        HidePauseButton();
    }

    private void StartCardSelectionUI()
    {
        HideHUD();
        ScreenDimmingOn();
        _cardSelectionUI.SetActive(true);
    }

    private void FinishCardSelectionUI()
    {
        ShowHUD();
        ScreenDimmingOff();
        _cardSelectionUI.SetActive(false);
    }

    private void SetUIText(Transform parent, string childName, string text)
    {
        var childObj = parent.Find(childName);
        if (childObj != null)
        {
            var textComponent = childObj.GetComponent<TextMeshProUGUI>();
            textComponent.text = text;
        }
    }

    private void UpdateHPBar(float currentHP) => _HPBarFilling.fillAmount = currentHP;

    private void UpdateScoreUI(int score) => _scoreText.text = $"score:\n{score:D8}";

    public void ScreenDimmingOn() => _screenDimming.SetActive(true);
    public void ScreenDimmingOff() => _screenDimming.SetActive(false);

    public void ShowScore() => _scoreText.gameObject.SetActive(true);
    public void HideScore() => _scoreText.gameObject.SetActive(false);

    public void ShowHPBar() => _HPBarUI.SetActive(true);
    public void HideHPBar() => _HPBarUI.SetActive(false);

    public void ShowPauseButton() => _pauseButton.gameObject.SetActive(true);
    public void HidePauseButton() => _pauseButton.gameObject.SetActive(false);

    public void ShowAudioSlider() => _audioUI.SetActive(true);
    public void HideAudioSlider() => _audioUI.SetActive(false);

    public void ShowGameOverUI() => _gameOverUI.SetActive(true);
    public void HideGameOverUI() => _gameOverUI.SetActive(false);

    public void ShowFinishGameUI() => _finishGameUI.SetActive(true);
    public void HideFinishGameUI() => _finishGameUI.SetActive(false);
}
