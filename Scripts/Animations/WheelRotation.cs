using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    private float _speedMultiplier = 10;

    void Update()
    {
        if (!PlayerController.Instance.IsGameOver)
            RotateWheel();
    }

    private void RotateWheel()
    {
        var rotationVector = Vector3.back * MoveLeft.GetCurrentSpeedLeft() *
            _speedMultiplier * Time.deltaTime;
        transform.Rotate(rotationVector);
    }
}
