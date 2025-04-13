using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _jumpButton;
    private bool _jumpPressed;

    private void Awake()
    {
        _jumpButton.onClick.AddListener(OnJumpButtonPressed);
        HideMobileInputUI();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart += ShowMobileInputUI;
        GameManager.Instance.OnFinishGame += HideMobileInputUI;
        GameManager.Instance.OnGameOver += HideMobileInputUI;
        GameManager.Instance.OnGamePaused += HideMobileInputUI;
        GameManager.Instance.OnGameContinued += ShowMobileInputUI;
        CardManager.Instance.OnStartCardSelection += HideMobileInputUI;
        CardManager.Instance.OnFinishCardSelection += ShowMobileInputUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= ShowMobileInputUI;
        GameManager.Instance.OnFinishGame -= HideMobileInputUI;
        GameManager.Instance.OnGameOver -= HideMobileInputUI;
        GameManager.Instance.OnGamePaused -= HideMobileInputUI;
        GameManager.Instance.OnGameContinued -= ShowMobileInputUI;
        CardManager.Instance.OnStartCardSelection -= HideMobileInputUI;
        CardManager.Instance.OnFinishCardSelection -= ShowMobileInputUI;
    }

    public bool IsJumpPressed()
    {
        if (_jumpPressed)
        {
            _jumpPressed = false;
            return true;
        }
        return false;
    }

    private void ShowMobileInputUI()
    {
        _joystick?.gameObject.SetActive(true);
        _jumpButton?.gameObject.SetActive(true);
    }

    private void HideMobileInputUI()
    {
        _joystick?.gameObject.SetActive(false);
        _jumpButton?.gameObject.SetActive(false);
    }

    private void OnJumpButtonPressed() => _jumpPressed = true;

    public float GetHorizontal() => _joystick.Horizontal;

    public float GetVertical() => _joystick.Vertical;
}
