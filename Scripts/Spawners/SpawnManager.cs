using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public List<GameObject> Obstacles;
    public List<GameObject> AccessibleObstacles;

    private float _defaultMinTimeToResp = 0.3f;
    private float _defaultMaxTimeToResp = 0.7f;
    private float _minTimeToResp;
    private float _maxTimeToResp;
    private float _pauseSpawnTime = 10;
    private Coroutine _spawnCoroutine;
    private static readonly float[] Roads = GameConstants.Roads;

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
        AccessibleObstacles.AddRange(Obstacles);
        _minTimeToResp = _defaultMinTimeToResp;
        _maxTimeToResp = _defaultMaxTimeToResp;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += EnableSpawnObstacle;
        GameManager.Instance.OnGameOver += DisableSpawnObstacle;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= EnableSpawnObstacle;
        GameManager.Instance.OnGameOver -= DisableSpawnObstacle;
    }

    private IEnumerator SpawnObstacleCourutine()
    {
        while (true)
        {
            float timeToNextObstacle = Random.Range(_minTimeToResp, _maxTimeToResp);
            yield return new WaitForSeconds(timeToNextObstacle);
            var obstacle = AccessibleObstacles[Random.Range(0, AccessibleObstacles.Count)];
            Instantiate(obstacle, GetRandomPosition(obstacle.transform.position.y),
                obstacle.transform.rotation);
        }
    }

    public void RemoveElementFromSpawn(string tagToRemove) =>
        AccessibleObstacles.RemoveAll(el => el.tag == tagToRemove);

    public void AddElementToSpawn(string tagToAdd)
    {
        GameObject elementToAdd = Obstacles
            .FirstOrDefault(el => el.tag == tagToAdd);
        if (elementToAdd != null)
            AccessibleObstacles.Add(elementToAdd);
    }

    public void ResetSpeedSpawnObstacle()
    {
        _minTimeToResp = _defaultMinTimeToResp;
        _maxTimeToResp = _defaultMaxTimeToResp;
    }

    public void SpeedUpSpawnObstacle(float speedUpValue)
    {
        _minTimeToResp /= speedUpValue;
        _maxTimeToResp /= speedUpValue;
    }

    public void PauseSpawnObstacle() => StartCoroutine(PauseSpawnObstacleCourutine());

    private IEnumerator PauseSpawnObstacleCourutine()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
        yield return new WaitForSeconds(_pauseSpawnTime);
        _spawnCoroutine = StartCoroutine(SpawnObstacleCourutine());
    }

    private Vector3 GetRandomPosition(float yPos)
    {
        var x = transform.position.x;
        var z = Roads[Random.Range(0, Roads.Length)];
        return new Vector3(x, yPos, z);
    }

    private void EnableSpawnObstacle()
    {
        if (_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(SpawnObstacleCourutine());
    }

    private void DisableSpawnObstacle()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }
}
