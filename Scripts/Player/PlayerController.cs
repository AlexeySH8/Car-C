using Assets.Scripts.Collidables;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public byte CurrentCountRepeats { get; private set; }
    public bool IsOnRoad { get; private set; }

    public PlayerScore PlayerScore;
    public PlayerHP HealthPoints;
    public PlayerPowerups Powerups;
    public GameObject SpeedTrail_FX;
    public GameObject Jump_FX;

    private Rigidbody _playerRb;
    private PlayerMovement _movement;
    private PlayerJump _jump;
    private bool _isJumpRequested;
    private float _verticalInput;
    private float _horizontalInput;
    private bool _canMove;

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
        Powerups = GetComponent<PlayerPowerups>();
    }

    private void Start()
    {
        ResetPosition();
        ResetPlayerParameters();
        _playerRb = GetComponent<Rigidbody>();
        _movement = GetComponent<PlayerMovement>();
        _jump = GetComponent<PlayerJump>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += EnableMovement;
        GameManager.Instance.OnGameOver += DisableMovement;
        GameManager.Instance.OnFinishGame += DisableMovement;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= EnableMovement;
        GameManager.Instance.OnGameOver -= DisableMovement;
        GameManager.Instance.OnFinishGame -= DisableMovement;
    }

    private void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsOnRoad)
                _isJumpRequested = true;
            else if (!IsOnRoad && !Powerups.IsTimeDilationActive
                && Powerups.IsPerkTimeDilationActive)
                TimeManager.Instance.SlowDownTime();
        }
    }

    void FixedUpdate()
    {
        if (_canMove)
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

    private void ResetPlayerParameters()
    {
        ResetCityRepeats();
        Powerups.ResetPowerups();
        PlayerScore.ResetCurrentScoreAmount();
        SetOnRoad(true);
        _isJumpRequested = false;
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

    private void ResetPosition() => transform.position = GameConstants.StartingPlayerCarPosition;

    public void SetOnRoad(bool value) => IsOnRoad = value;

    public void IncreaseCityRepeats() => CurrentCountRepeats++;

    public void ResetCityRepeats() => CurrentCountRepeats = 0;

    private void EnableMovement() => _canMove = true;

    private void DisableMovement() => _canMove = false;
}
