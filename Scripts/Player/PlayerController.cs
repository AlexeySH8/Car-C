using Assets.Scripts.Collidables;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public PlayerScore PlayerScore;
    public PlayerHP HealthPoints;
    public GameObject SpeedTrail_FX;
    public GameObject Jump_FX;

    public byte CurrentCountRepeats { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsOnRoad { get; private set; }
    public bool IsSpeedPowerupActive { get; private set; }
    public bool IsJumpPowerupActive { get; private set; }
    public bool IsTimeDilationActive { get; private set; }
    public bool IsPerkIncreaseHPActive { get; private set; }
    public bool IsPerkTimeDilationActive { get; private set; }

    private bool _isJumpRequested;
    private float _verticalInput;
    private float _horizontalInput;

    private Rigidbody _playerRb;
    private PlayerMovement _movement;
    private PlayerJump _jump;   

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        HealthPoints = GetComponent<PlayerHP>();
        PlayerScore = GetComponent<PlayerScore>();
    }

    private void Start()
    {
        ResetCityRepeats();
        SpeedPowerupOff();
        JumpPowerupOff();
        TimeDilationOff();
        PerkIncreaseHPOff();
        PerkTimeDilationOff();
        SetOnRoad(true);
        IsGameOver = false;
        _isJumpRequested = false;
        _playerRb = GetComponent<Rigidbody>();
        _movement = GetComponent<PlayerMovement>();
        _jump = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsOnRoad)
                _isJumpRequested = true;
            else if (!IsOnRoad && !IsTimeDilationActive
                && IsPerkTimeDilationActive)
                TimeManager.Instance.SlowDownTime();
        }
    }

    void FixedUpdate()
    {
        if (!IsGameOver)
        {
            if (_isJumpRequested)
            {
                IsOnRoad = false;
                _isJumpRequested = false;
                _jump.JumpCar();
            }
            _movement.MoveCar(_horizontalInput, _verticalInput, IsOnRoad);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollidable collidable))
            collidable.CollisionWithPlayer(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ICollidable collidable))
            collidable.CollisionWithPlayer(this);
    }

    public void SetGameOver() => IsGameOver = true;

    public void SetOnRoad(bool value) => IsOnRoad = value;

    public void IncreaseCityRepeats() => CurrentCountRepeats++;

    public void ResetCityRepeats() => CurrentCountRepeats = 0;

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
