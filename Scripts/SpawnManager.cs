using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public bool SpawnOn = true;
    public GameObject[] Obstacles;

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
        _minTimeToResp = _defaultMinTimeToResp;
        _maxTimeToResp = _defaultMaxTimeToResp;
        _spawnCoroutine = StartCoroutine(SpawnObstacleCourutine());
    }

    private IEnumerator SpawnObstacleCourutine()
    {
        while (SpawnOn && !PlayerController.Instance.IsGameOver)
        {
            float timeToNextObstacle = Random.Range(_minTimeToResp, _maxTimeToResp);
            yield return new WaitForSeconds(timeToNextObstacle);
            var obstacle = Obstacles[Random.Range(0, Obstacles.Length)];
            Instantiate(obstacle, GetRandomPosition(obstacle.transform.position.y),
                obstacle.transform.rotation);
        }
    }

    public void ResetSpeedSpawnObstacle()
    {
        _minTimeToResp = _defaultMinTimeToResp;
        _maxTimeToResp = _defaultMaxTimeToResp;
    }

    public void SpeedUpSpawnObstacle()
    {
        _minTimeToResp /= 2;
        _maxTimeToResp /= 2;
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
}
