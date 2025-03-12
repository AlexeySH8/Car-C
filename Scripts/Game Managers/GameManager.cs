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
    [SerializeField] private PlayableDirector _playableDirector;

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
        UIManager.Instance.ShowStartGameUI();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCourutine());
    }

    private IEnumerator StartGameCourutine()
    {
        UIManager.Instance.HideStartGameUI();
        if (_playableDirector != null)
        {
            _playableDirector.Play();
            yield return new WaitUntil(() => _playableDirector.state != PlayState.Playing);
        }
        OnGameStart?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
