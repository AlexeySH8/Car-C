using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    private float _speedMultiplier = 10;
    private bool _canRotate;

    void Update()
    {
        if (_canRotate)
            RotateWheel();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += EnableRotation;
        GameManager.Instance.OnGameOver += DisableRotation;
        GameManager.Instance.OnFinishGame += DisableRotation;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= EnableRotation;
        GameManager.Instance.OnGameOver -= DisableRotation;
        GameManager.Instance.OnFinishGame -= DisableRotation;
    }

    private void RotateWheel()
    {
        var rotationVector = Vector3.back * MoveLeft.GetCurrentSpeedLeft() *
            _speedMultiplier * Time.deltaTime;
        transform.Rotate(rotationVector);
    }

    private void EnableRotation() => _canRotate = true;

    private void DisableRotation() => _canRotate = false;
}
