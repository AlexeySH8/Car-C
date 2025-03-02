using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnAds : MonoBehaviour
{
    public static SpawnAds Instance { get; private set; }
    public bool SpawnOn = true;
    [SerializeField] private GameObject[] _backAds;
    [SerializeField] private GameObject[] _frontAds;
    public GameObject[] AllAds { get; private set; }

    private Vector3 _backAdsPosition;
    private float _minTimeToRespBack = 1.5f; // 1.5
    private float _maxTimeToRespBack = 3; // 3
    private float _backAdsY = 23.4f;

    private Vector3 _frontAdsPosition;
    private float _minTimeToRespFront = 0.5f;
    private float _maxTimeToRespFront = 1;
    private float _frontAdsY = -9f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        AllAds = _frontAds.Concat(_backAds).ToArray();
    }

    void Start()
    {
        _backAdsPosition = new Vector3(transform.position.x,
            _backAdsY, -transform.position.z);
        _frontAdsPosition = new Vector3(transform.position.x,
            _frontAdsY, transform.position.z);
        StartCoroutine(SpawnFrontAdsCourutine());
        StartCoroutine(SpawnBackAdsCourutine());
    }

    private IEnumerator SpawnBackAdsCourutine()
    {
        while (SpawnOn && !PlayerController.Instance.IsGameOver)
        {
            float timeToNextAds = Random.Range(_minTimeToRespBack, _maxTimeToRespBack);
            yield return new WaitForSeconds(timeToNextAds);
            GameObject adsFront = _backAds[Random.Range(0, _backAds.Length)];
            Instantiate(adsFront, _backAdsPosition, transform.rotation);
        }
    }

    private IEnumerator SpawnFrontAdsCourutine()
    {
        while (SpawnOn && !PlayerController.Instance.IsGameOver)
        {
            float timeToNextAds = Random.Range(_minTimeToRespFront, _maxTimeToRespFront);
            yield return new WaitForSeconds(timeToNextAds);
            GameObject adsFront = _frontAds[Random.Range(0, _frontAds.Length)];
            Instantiate(adsFront, _frontAdsPosition, transform.rotation);
        }
    }

}
