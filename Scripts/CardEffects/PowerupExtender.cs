using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupExtender : ICardEffect
{
    private sbyte _additionalDuration = 2;

    public void ExecuteEffect()
    {
        SpeedPowerup.ResetSpeedDuration();
        JumpPowerup.ResetSpeedDuration();
        SpeedPowerup.IncreaseSpeedDuration(_additionalDuration);
        JumpPowerup.IncreaseSpeedDuration(_additionalDuration);
    }
}
