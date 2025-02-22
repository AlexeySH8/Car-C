using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    private sbyte _currentHP;
    private sbyte _maxHP;
    private sbyte _damageTaken = 1;
    private sbyte _healingAmount = 1;

    void Start()
    {
        _maxHP = 100;
        _currentHP = _maxHP;
    }

    public void TakeHP()
    {
        if (_currentHP < _maxHP)
            _currentHP += _healingAmount;
        //Debug.Log(_currentHP);
    }

    public void TakeDamage()
    {
        _currentHP -= _damageTaken;
       // Debug.Log(_currentHP);
    }

    public bool IsPlayerDead() => _currentHP <= 0;
}
