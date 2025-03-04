using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHP : ICardEffect
{
    private sbyte _increasedHP = 100;

    public void ExecuteEffect()
    {
        UIManager.Instance.ShowHPBar();
        SpawnManager.Instance.AddElementToSpawn("HPPowerup");
        PlayerController.Instance
            .PerkIncreaseHPOn();
        PlayerController.Instance
            .HealthPoints.ChangeMaxHPValue(_increasedHP);
        PlayerController.Instance
            .PlayerScore.ResetCurrentScoreAmount();
    }
}
