using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.Rendering.HableCurve;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _screenDimming;
    [SerializeField] private GameObject _cardSelectionUI;
    [SerializeField] private GameObject _HPBarUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _HPBarFilling;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerController.Instance.PlayerScore.OnScoreChanged += UpdateScoreUI;
        PlayerController.Instance.HealthPoints.OnHPChanged += UpdateHPBar;
    }

    private void OnDisable()
    {
        PlayerController.Instance.PlayerScore.OnScoreChanged -= UpdateScoreUI;
        PlayerController.Instance.HealthPoints.OnHPChanged -= UpdateHPBar;
    }

    private void UpdateHPBar(float currentHP) => _HPBarFilling.fillAmount = currentHP;

    private void UpdateScoreUI(int score) => _scoreText.text = $"score:\n{score:D8}";

    public void ScreenDimmingOn() => _screenDimming.SetActive(true);

    public void ScreenDimmingOff() => _screenDimming.SetActive(false);

    public void ShowScore() => _scoreText.gameObject.SetActive(true);

    public void HideScore() => _scoreText.gameObject.SetActive(false);

    public void ShowCardSelection() => _cardSelectionUI.SetActive(true);

    public void HideCardSelection() => _cardSelectionUI.SetActive(false);

    public void ShowHPBar() => _HPBarUI.SetActive(true);

    public void HideHPBar() => _HPBarUI.SetActive(false);
}
