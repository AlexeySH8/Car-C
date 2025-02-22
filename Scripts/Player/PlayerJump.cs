using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpForce { get; private set; }
    private float defaultJumpForce = 5500;
    private Rigidbody _playerRb;

    void Start()
    {
        JumpForce = defaultJumpForce;
        _playerRb = GetComponent<Rigidbody>();
    }

    public void JumpCar()
    {
        _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    public void IncreaseJumpForce(float jumpMultiplier) => JumpForce *= jumpMultiplier;

    public void ResetJumpForce() => JumpForce = defaultJumpForce;
}
