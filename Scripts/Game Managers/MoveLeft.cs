using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private static float _defaultSpeed = 300; // 150
    private static bool _canMoveLeftl;
    private static float _currentSpeedLeft = _defaultSpeed;
    private Rigidbody _objectRb;

    private void Start()
    {
        _objectRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += EnableMoveLeft;
        GameManager.Instance.OnGameOver += DisableMoveLeft;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= EnableMoveLeft;
        GameManager.Instance.OnGameOver -= DisableMoveLeft;
    }

    private void FixedUpdate()
    {
        if (_canMoveLeftl)
        {
            Vector3 newPosition = _objectRb.position + Vector3.left *
                Time.fixedDeltaTime * _currentSpeedLeft;
            _objectRb.MovePosition(newPosition);
        }
    }

    public static void IncreaseSpeed(float speedMultiplier)
        => _currentSpeedLeft *= speedMultiplier;

    public static float GetCurrentSpeedLeft() => _currentSpeedLeft;

    public static void ResetSpeedToDefault() => _currentSpeedLeft = _defaultSpeed;

    private void EnableMoveLeft() => _canMoveLeftl = true;

    private void DisableMoveLeft() => _canMoveLeftl = false;
}
