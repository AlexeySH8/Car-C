using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupExtender : ICardEffect
{
    private sbyte _additionalDuration = 2;

    public void ExecuteEffect()
    {
        SpeedPowerup.ResetEffectDuration();
        JumpPowerup.ResetEffectDuration();
        TimeManager.ResetEffectDuration();
        SpeedPowerup.IncreaseEffectDuration(_additionalDuration);
        JumpPowerup.IncreaseEffectDuration(_additionalDuration);
        TimeManager.IncreaseEffectDuration(_additionalDuration);
    }
}
