using System.Collections;
using System.Collections.Generic;
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

    private IEnumerator SlowDownTimeCourutine()
    {
        PlayerController.Instance.TimeDilationOn();
        Time.timeScale /= _timeDevisior;
        yield return new WaitForSeconds(_durationDilationTime / _timeDevisior);
        ResetTimeToDefault();
        yield return new WaitForSeconds(_cooldownDilationTime);
        PlayerController.Instance.TimeDilationOff();
    }

    public void SlowDownTime() => StartCoroutine(SlowDownTimeCourutine());

    public void PauseGame() => Time.timeScale = 0;

    public void ContinueGame() => ResetTimeToDefault();

    private void ResetTimeToDefault() => Time.timeScale = 1;

    public static void IncreaseEffectDuration(float additionalDuration)
        => _durationDilationTime += additionalDuration;

    public static void ResetEffectDuration() => _durationDilationTime = _defaultDurationDilationTime;
}
