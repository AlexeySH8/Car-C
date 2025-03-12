using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceHPTo1 : ICardEffect
{
    private sbyte _multiplier = 100;

    public void ExecuteEffect()
    {
        UIManager.Instance.HideHPBar();
        PlayerController.Instance
            .Powerups.PerkIncreaseHPOff();
        SpawnManager.Instance
            .RemoveElementFromSpawn("HPPowerup");
        PlayerController.Instance
            .HealthPoints.ChangeMaxHPValue(1);
        PlayerController.Instance
            .PlayerScore.IncreaseCurrentScoreAmount(_multiplier);
    }
}
