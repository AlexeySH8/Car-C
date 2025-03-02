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
        _maxHP = 3;
        _currentHP = _maxHP;
    }

    public void TakeHP()
    {
        if (_currentHP < _maxHP)
            _currentHP += _healingAmount;
        HPChanged();
    }

    public void TakeDamage()
    {
        _currentHP -= _damageTaken;
        if (IsPlayerDead())
            PlayerController.Instance.SetGameOver();
        HPChanged();
    }

    public void ChangeMaxHPValue(sbyte newValue)
    {
        _currentHP = newValue;
        _maxHP = newValue;
        HPChanged();
    }

    private void HPChanged()
    {
        float currentHPAsPercantage = (float)_currentHP / (float)_maxHP;
        OnHPChanged?.Invoke(currentHPAsPercantage);
    }

    public bool IsPlayerDead() => _currentHP <= 0;
}
