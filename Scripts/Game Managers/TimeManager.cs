using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private static float _defaultDurationDilationTime = 3f;
    private static float _durationDilationTime = _defaultDurationDilationTime;
    private float _cooldownDilationTime = 1f;
    private float _timeDevisior = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        CardManager.Instance.OnStartCardSelection += PauseGame;
        CardManager.Instance.OnFinishCardSelection += ContinueGame;
        GameManager.Instance.OnGamePaused += PauseGame;
        GameManager.Instance.OnGameContinued += ContinueGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGamePaused -= PauseGame;
        GameManager.Instance.OnGameContinued -= ContinueGame;
        CardManager.Instance.OnStartCardSelection -= PauseGame;
        CardManager.Instance.OnFinishCardSelection -= ContinueGame;
    }

    private IEnumerator SlowDownTimeCourutine()
    {
        AudioManager.Instance.PlayTimeDilationSFX();
        PlayerController.Instance.Powerups.TimeDilationOn();
        Time.timeScale /= _timeDevisior;
        yield return new WaitForSeconds(_durationDilationTime / _timeDevisior);
        ResetTimeToDefault();
        yield return new WaitForSeconds(_cooldownDilationTime);
        PlayerController.Instance.Powerups.TimeDilationOff();
    }

    public void SlowDownTime() => StartCoroutine(SlowDownTimeCourutine());

    public void PauseGame() => Time.timeScale = 0;

    public void ContinueGame() => ResetTimeToDefault();

    private void ResetTimeToDefault() => Time.timeScale = 1;

    public static void IncreaseEffectDuration(float additionalDuration)
        => _durationDilationTime += additionalDuration;

    public static void ResetEffectDuration() => _durationDilationTime = _defaultDurationDilationTime;
}
