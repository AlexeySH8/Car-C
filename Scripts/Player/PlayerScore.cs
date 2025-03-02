using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScore : MonoBehaviour
{
    public event Action<int> OnScoreChanged;

    public int _currentScoreAmount;
    private int _score;
    private float _scoreTimer = 0f;
    private float _scoreRate = 0.01f;
    private int _defaultScoreAmount = 1;

    private void Start()
    {
        _score = 0;
        _currentScoreAmount = _defaultScoreAmount;
    }

    private void Update()
    {
        _scoreTimer += Time.deltaTime;
        if (_scoreTimer >= _scoreRate && !PlayerController.Instance.IsGameOver)
        {
            AddScore();
            _scoreTimer = 0f;
        }
    }

    public void AddScore()
    {
        if (_score + _currentScoreAmount < 99999999)
            _score += _currentScoreAmount;
        else
            _score = 99999999;
        OnScoreChanged?.Invoke(_score);
    }

    public void IncreaseCurrentScoreAmount(sbyte multiplier)
    {
        ResetCurrentScoreAmount();
        _currentScoreAmount *= multiplier;
    }

    public void ResetCurrentScoreAmount() => _currentScoreAmount = _defaultScoreAmount;
}
