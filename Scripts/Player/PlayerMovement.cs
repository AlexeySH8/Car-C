using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _zBoundary = GameConstants.Roads[GameConstants.Roads.Length - 1];
    private float _xBoundary = 40;

    private float _verticalSpeed = 80.0f;  // 80
    private float _horizontalSpeed = 40.0f; // 40
    private float _flightSpeed = 30.0f;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void MoveCar(float horizontalInput, float verticalInput, bool isOnRoad)
    {
        var currentSpeed = isOnRoad ? _verticalSpeed : _flightSpeed;
        var newPosition = playerRb.position +
        Vector3.forward * verticalInput * currentSpeed * Time.fixedDeltaTime +
        Vector3.right * horizontalInput * _horizontalSpeed * Time.fixedDeltaTime;
        newPosition.z = Mathf.Clamp(newPosition.z, -_zBoundary, _zBoundary);
        newPosition.x = Mathf.Clamp(newPosition.x, -_xBoundary, _xBoundary);
        playerRb.MovePosition(newPosition);
    }
}
