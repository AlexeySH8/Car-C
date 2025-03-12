using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScore : MonoBehaviour
{
    public event Action<int> OnScoreChanged;

    private bool _canAddScore;
    private int _currentScoreAmount;
    private int _score;
    private float _scoreTimer = 0f;
    private float _scoreRate = 0.01f;
    private int _defaultScoreAmount = 1;

    private void Start()
    {
        _score = 0;
        _currentScoreAmount = _defaultScoreAmount;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += EnableAddScore;
        GameManager.Instance.OnGameOver += DisableAddScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= EnableAddScore;
        GameManager.Instance.OnGameOver -= DisableAddScore;
    }

    private void Update()
    {
        if (_canAddScore && !PlayerController.Instance.Powerups.IsPerkIncreaseHPActive)
        {
            _scoreTimer += Time.deltaTime;
            if (_scoreTimer >= _scoreRate)
            {
                AddScore(_currentScoreAmount);
                _scoreTimer = 0f;
            }
        }
    }

    public void AddScore(int currentScoreAmount)
    {
        if (!_canAddScore || currentScoreAmount < 0) return;
        int newScore = Math.Min(_score + currentScoreAmount, 99999999);
        if (newScore != _score)
        {
            _score = newScore;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public void IncreaseCurrentScoreAmount(sbyte multiplier)
    {
        ResetCurrentScoreAmount();
        _currentScoreAmount *= multiplier;
    }

    public void ResetCurrentScoreAmount() => _currentScoreAmount = _defaultScoreAmount;

    public int GetCurrentScore() => _score;

    private void EnableAddScore() => _canAddScore = true;

    private void DisableAddScore() => _canAddScore = false;
}
