using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public event Action<float> OnHPChanged;

    private sbyte _currentHP;
    private sbyte _maxHP;
    private sbyte _damageTaken = 1;
    private sbyte _healingAmount = 1;

    void Start()
    {
        _maxHP = 5;
        _currentHP = _maxHP;
    }

    public void TakeHP()
    {
        if (_currentHP < _maxHP)
            _currentHP += _healingAmount;
        OnHPChanged?.Invoke(GetCurrentHPAsPercantage());
    }

    public void TakeDamage()
    {
        _currentHP -= _damageTaken;
        if (IsPlayerDead())
            PlayerController.Instance.SetGameOver();
        OnHPChanged?.Invoke(GetCurrentHPAsPercantage());
    }

    public void ChangeMaxHPValue(sbyte newValue)
    {
        _currentHP = newValue;
        _maxHP = newValue;
        OnHPChanged?.Invoke(GetCurrentHPAsPercantage());
    }

    private float GetCurrentHPAsPercantage() => (float)_currentHP / (float)_maxHP;

    public bool IsPlayerDead() => _currentHP <= 0;
}
