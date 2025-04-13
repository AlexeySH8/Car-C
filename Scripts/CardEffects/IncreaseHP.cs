using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHP : ICardEffect
{
    private byte _increasedHP = 100;

    public void ExecuteEffect()
    {
        UIManager.Instance.ShowHPBar();
        SpawnManager.Instance.AddElementToSpawn("HPPowerup");
        PlayerController.Instance
            .Powerups.PerkIncreaseHPOn();
        PlayerController.Instance
            .HealthPoints.ChangeMaxHPValue(_increasedHP);
        PlayerController.Instance.PlayerStatistics
            .PlayerScore.ResetCurrentScoreAmount();
    }
}
