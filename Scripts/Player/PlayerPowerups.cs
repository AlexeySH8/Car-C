using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerups : MonoBehaviour
{
    public bool IsSpeedPowerupActive { get; private set; }
    public bool IsJumpPowerupActive { get; private set; }
    public bool IsTimeDilationActive { get; private set; }
    public bool IsPerkIncreaseHPActive { get; private set; }
    public bool IsPerkTimeDilationActive { get; private set; }

    public void ResetPowerups()
    {
        SpeedPowerupOff();
        JumpPowerupOff();
        TimeDilationOff();
        PerkIncreaseHPOff();
        PerkTimeDilationOff();
    }

    public void SpeedPowerupOn() => IsSpeedPowerupActive = true;
    public void SpeedPowerupOff() => IsSpeedPowerupActive = false;

    public void JumpPowerupOn() => IsJumpPowerupActive = true;
    public void JumpPowerupOff() => IsJumpPowerupActive = false;

    public void TimeDilationOn() => IsTimeDilationActive = true;
    public void TimeDilationOff() => IsTimeDilationActive = false;

    public void PerkIncreaseHPOn() => IsPerkIncreaseHPActive = true;
    public void PerkIncreaseHPOff() => IsPerkIncreaseHPActive = false;

    public void PerkTimeDilationOn() => IsPerkTimeDilationActive = true;
    public void PerkTimeDilationOff() => IsPerkTimeDilationActive = false;
}
