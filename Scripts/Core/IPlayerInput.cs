using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    float GetHorizontal();
    float GetVertical();
    bool IsJumpPressed();
}
