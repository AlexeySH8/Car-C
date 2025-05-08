using System;
using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public event Action<float> OnHPChanged;

    private bool _canTakeDamage;
    private byte _currentHP;
    private byte _maxHP;
    private byte _damageTaken = 1;
    private byte _healingAmount = 1;
    private float _invulnerabilityTime = 0.5f;

    void Start()
    {
        _maxHP = 3;
        _currentHP = _maxHP;
        _canTakeDamage = true;

        GameManager.Instance.OnGameStart += EnableDamage;
        GameManager.Instance.OnFinishGame += DisableDamage;
        GameManager.Instance.OnGameOver += DisableDamage;
        CardManager.Instance.OnFinishCardSelection += ActivateTemporaryInvincibility;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= EnableDamage;
        GameManager.Instance.OnFinishGame -= DisableDamage;
        GameManager.Instance.OnGameOver -= DisableDamage;
        CardManager.Instance.OnFinishCardSelection -= ActivateTemporaryInvincibility;
    }

    public void TakeHP()
    {
        if (_currentHP < _maxHP)
            _currentHP += _healingAmount;
        OnHPChanged?.Invoke(GetCurrentHPAsPercantage());
    }

    public void TakeDamage()
    {
        if (_canTakeDamage)
        {
            _currentHP -= _damageTaken;
            if (IsPlayerDead())
            {
                ADSManager.Instance.LaunchRewardedAd(adWatched =>
                {
                    if (adWatched)
                    {
                        _currentHP = _maxHP;
                        OnHPChanged?.Invoke(GetCurrentHPAsPercantage());
                        return;
                    }
                    GameManager.Instance.GameOver();
                });
            }
            OnHPChanged?.Invoke(GetCurrentHPAsPercantage());
        }
    }

    public void ChangeMaxHPValue(byte newValue)
    {
        _currentHP = newValue;
        _maxHP = newValue;
        OnHPChanged?.Invoke(GetCurrentHPAsPercantage());
    }

    private IEnumerator ActivateTemporaryInvincibilityCoroutine()
    {
        DisableDamage();
        yield return new WaitForSeconds(_invulnerabilityTime);
        EnableDamage();
    }

    public void ActivateTemporaryInvincibility() =>
        StartCoroutine(ActivateTemporaryInvincibilityCoroutine());

    private float GetCurrentHPAsPercantage() => (float)_currentHP / (float)_maxHP;

    private bool IsPlayerDead() => _currentHP <= 0;

    private void DisableDamage() => _canTakeDamage = false;

    private void EnableDamage() => _canTakeDamage = true;
}
