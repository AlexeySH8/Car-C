using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpForce { get; private set; }
    private float _defaultJumpForce = 5500;
    private Rigidbody _playerRb;

    void Start()
    {
        JumpForce = _defaultJumpForce;
        _playerRb = GetComponent<Rigidbody>();
    }

    public void JumpCar()
    {
        _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    public void IncreaseJumpForce(float jumpMultiplier) => JumpForce *= jumpMultiplier;

    public void ResetJumpForce() => JumpForce = _defaultJumpForce;
}
