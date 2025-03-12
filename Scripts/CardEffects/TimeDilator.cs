using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilator : ICardEffect
{
    public void ExecuteEffect()
    {
        PlayerController.Instance.Powerups.PerkTimeDilationOn();
    }
}
