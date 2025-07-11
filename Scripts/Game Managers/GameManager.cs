using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnGameOver;
    public event Action OnGameStart;
    public event Action OnFinishGame;
    public event Action OnGamePaused;
    public event Action OnGameContinued;

    private float _delayToRestart = 0.2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private IEnumerator StartGameCourutine()
    {
        UIManager.Instance.HideMenuGameUI();
        yield return CutsceneManager.Instance.PlayStartCutscene();
        OnGameStart?.Invoke();
    }

    private IEnumerator FinishGameCourutine()
    {
        OnFinishGame?.Invoke();
        yield return CutsceneManager.Instance.PlayFinishCutscene();
        UIManager.Instance.ShowFinishGameUI();
    }

    private IEnumerator RestartWithDelay()
    {
        yield return new WaitForSeconds(_delayToRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame() => StartCoroutine(RestartWithDelay());

    public void FinishGame() => StartCoroutine(FinishGameCourutine());

    public void StartGame() => StartCoroutine(StartGameCourutine());

    public void GameOver() => OnGameOver?.Invoke();

    public void PauseGame() => OnGamePaused?.Invoke();

    public void ContinueGame() => OnGameContinued?.Invoke();
}
