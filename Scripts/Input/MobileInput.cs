using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour, IPlayerInput
{
    public static MobileInput Instance { get; private set; }

    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _jumpButton;
    private bool _jumpPressed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _jumpButton.onClick.AddListener(OnJumpButtonPressed);
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

    public void ShowMobileInputUI()
    {
        _joystick?.gameObject.SetActive(true);
        _jumpButton?.gameObject.SetActive(true);
    }

    public void HideMobileInputUI()
    {
        _joystick?.gameObject.SetActive(false);
        _jumpButton?.gameObject.SetActive(false);
    }

    private void OnJumpButtonPressed() => _jumpPressed = true;

    public float GetHorizontal() => _joystick.Horizontal;

    public float GetVertical() => _joystick.Vertical;
}
