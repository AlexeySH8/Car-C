using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : IPlayerInput
{
    public float GetHorizontal() => Input.GetAxis("Horizontal");
    public float GetVertical() => Input.GetAxis("Vertical");
    public bool IsJumpPressed() => Input.GetKeyDown(KeyCode.Space);
}
