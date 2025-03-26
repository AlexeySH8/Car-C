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
    [SerializeField] private GameObject _startGameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _finishGameUI;
    [SerializeField] private GameObject _HPBarUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _HPBarFilling;
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
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += HUD;
        GameManager.Instance.OnGameOver += GameOverUI;
        PlayerController.Instance.PlayerStatistics.PlayerScore.OnScoreChanged += UpdateScoreUI;
        PlayerController.Instance.HealthPoints.OnHPChanged += UpdateHPBar;
        CardManager.Instance.OnStartCardSelection += StartCardSelectionUI;
        CardManager.Instance.OnFinishCardSelection += FinishCardSelectionUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= HUD;
        GameManager.Instance.OnGameOver -= GameOverUI;
        PlayerController.Instance.PlayerStatistics.PlayerScore.OnScoreChanged -= UpdateScoreUI;
        PlayerController.Instance.HealthPoints.OnHPChanged -= UpdateHPBar;
        CardManager.Instance.OnStartCardSelection -= StartCardSelectionUI;
        CardManager.Instance.OnFinishCardSelection -= FinishCardSelectionUI;
    }

    private void HUD()
    {
        ShowHPBar();
        ShowScore();
        ScreenDimmingOff();
        HideGameOverUI();
        HideFinishGameUI();
    }

    private void GameOverUI()
    {
        HideHPBar();
        HideScore();
        ScreenDimmingOn();
        SetUIText(_gameOverUI.transform, "FinalScoreText",
            $"SCORE:\n{_playerStatistics.PlayerScore.GetCurrentScore():D8}");
        ShowGameOverUI();
    }

    public void FinishGameUI()
    {
        HideHPBar();
        HideScore();
        var parent = _finishGameUI.transform;

        SetUIText(parent, "FinalScoreText",
            $"SCORE:\n{_playerStatistics.PlayerScore.GetCurrentScore():D8}");

        SetUIText(parent, "ObstaclesDownText",
            $"OBSTACLES DOWN:\n{_playerStatistics.ObstaclesDown:D8}");

        SetUIText(parent, "Distance TraveledText",
            $"DISTANCE TRAVELED:\n{_playerStatistics.DistanceTraveled:D8}");

        ShowFinishGameUI();
    }

    private void StartCardSelectionUI()
    {
        ScreenDimmingOn();
        _cardSelectionUI.SetActive(true);

    }

    private void FinishCardSelectionUI()
    {
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

    public void ShowGameOverUI() => _gameOverUI.SetActive(true);
    public void HideGameOverUI() => _gameOverUI.SetActive(false);

    public void ShowStartGameUI() => _startGameUI.SetActive(true);
    public void HideStartGameUI() => _startGameUI.SetActive(false);

    public void ShowFinishGameUI() => _finishGameUI.SetActive(true);
    public void HideFinishGameUI() => _finishGameUI.SetActive(false);
}
