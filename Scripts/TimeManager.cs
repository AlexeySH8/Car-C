using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private float _durationDilationTime = 3f;
    private float _cooldownDilationTime = 5f;
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

    public void SlowDownTime() => StartCoroutine(SlowDownTimeCourutine());

    private IEnumerator SlowDownTimeCourutine()
    {
        PlayerController.Instance.TimeDilationOn();
        Time.timeScale /= _timeDevisior;
        yield return new WaitForSeconds(_durationDilationTime / _timeDevisior);
        ResetTimeToDefault();
        yield return new WaitForSeconds(_cooldownDilationTime);
        PlayerController.Instance.TimeDilationOff();
    }

    private void ResetTimeToDefault() => Time.timeScale = 1;
}
