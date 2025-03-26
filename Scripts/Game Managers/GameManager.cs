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

    private void Start()
    {
        ChangeColor.Instance.ChangeSceneMaterial();
        ChangeColor.Instance.ChangeAdsMaterials();
        UIManager.Instance.ShowStartGameUI();
    }

    private IEnumerator StartGameCourutine()
    {
        UIManager.Instance.HideStartGameUI();
        CutsceneManager.Instance.SetStartCutscene();
        if (CutsceneManager.Instance.PlayableDirector != null)
        {
            CutsceneManager.Instance.PlayableDirector.Play();
            yield return new WaitUntil(() => CutsceneManager.Instance.PlayableDirector.state != PlayState.Playing);
        }
        OnGameStart?.Invoke();
    }

    private IEnumerator FinishGameCourutine()
    {
        CutsceneManager.Instance.SetFinishCutscene();
        OnFinishGame?.Invoke();
        if (CutsceneManager.Instance.PlayableDirector != null)
        {
            CutsceneManager.Instance.PlayableDirector.Play();
            yield return new WaitUntil(() => CutsceneManager.Instance.PlayableDirector.state != PlayState.Playing);
        }
        UIManager.Instance.FinishGameUI();
    }

    public void RestartGame()
    {
        StartCoroutine(RestartWithDelay());
    }

    private IEnumerator RestartWithDelay()
    {
        yield return new WaitForSeconds(_delayToRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FinishGame() => StartCoroutine(FinishGameCourutine());

    public void StartGame() => StartCoroutine(StartGameCourutine());

    public void GameOver() => OnGameOver?.Invoke();
}
