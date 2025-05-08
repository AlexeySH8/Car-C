using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _screenDimming;
    [SerializeField] private GameObject _cardSelectionUI;
    [SerializeField] private GameObject _menuGameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _finishGameUI;
    [SerializeField] private GameObject _adsPanelUI;
    [SerializeField] private GameObject _audioUI;
    [SerializeField] private GameObject _pauseGameUI;
    [SerializeField] private GameObject _HPBarUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _HPBarFilling;
    [SerializeField] private Image _adsTimerFilling;
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
        GameManager.Instance.OnGameOver += ShowGameOverUI;
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
        GameManager.Instance.OnGameOver -= ShowGameOverUI;
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
        HideHUD();
        ScreenDimmingOn();
        ShowAudioSlider();
        _pauseGameUI.SetActive(true);
    }
    public void HidePauseGameUI()
    {
        ShowHUD();
        ScreenDimmingOff();
        HideAudioSlider();
        _pauseGameUI.SetActive(false);
    }

    private void ShowGameOverUI()
    {
        HideHUD();
        ScreenDimmingOn();
        SetUIText(_gameOverUI.transform, "FinalScoreText",
            $"SCORE:\n{_playerStatistics.PlayerScore.GetCurrentScore():D8}");

        _adsPanelUI.SetActive(false);
        _gameOverUI.SetActive(true);
    }

    public void ShowFinishGameUI()
    {
        HideHUD();
        var parent = _finishGameUI.transform;

        SetUIText(parent, "FinalScoreText",
            $"SCORE:\n{_playerStatistics.PlayerScore.GetCurrentScore():D8}");

        SetUIText(parent, "ObstaclesDownText",
            $"OBSTACLES DOWN:\n{_playerStatistics.ObstaclesDown:D8}");

        SetUIText(parent, "Distance TraveledText",
            $"DISTANCE TRAVELED:\n{_playerStatistics.DistanceTraveled:D8}");

        _finishGameUI.SetActive(true);
    }

    public void ShowAdsPanelUI()
    {
        ScreenDimmingOn();
        HideHUD();
        _adsPanelUI.SetActive(true);
    }

    public void HideAdsPanelUI()
    {
        ScreenDimmingOff();
        ShowHUD();
        _adsPanelUI.SetActive(false);
    }

    private void ShowHUD()
    {
        ShowHPBar();
        ShowScore();
        ShowPauseButton();
        MobileInput.Instance.ShowMobileInputUI();
    }

    public void HideHUD()
    {
        HideHPBar();
        HideScore();
        HidePauseButton();
        MobileInput.Instance.HideMobileInputUI();
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

    public void UpdateADSTimer(float currentTime) => _adsTimerFilling.fillAmount = currentTime;

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
}
