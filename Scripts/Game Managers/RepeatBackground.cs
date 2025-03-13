using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public static byte CurrentCity { get; private set; }
    private byte _requiredRepeatsToNextCity;
    private byte _minRepeats = 1; // 10
    private byte _maxRepeats = 1; // 20
    private float _repeatWidth;
    private float _cityWidth;
    private float _positionCurrentCity;
    private float _segmentOfCityWidth;
    private float _startingPosition;

    void Start()
    {
        CurrentCity = 0; // 0 1 2
        _requiredRepeatsToNextCity = GetRandomRequiredRepeats();
        _segmentOfCityWidth = GetComponent<BoxCollider>().size.x;
        _repeatWidth = _segmentOfCityWidth;
        _cityWidth = _segmentOfCityWidth * GameConstants.ÑitySegmentCount;
        _positionCurrentCity = PositionCurrentCity();
    }

    void Update()
    {
        RepeatCurrentBackground();
    }

    private void RepeatCurrentBackground()
    {
        if (transform.position.x < _positionCurrentCity - _repeatWidth)
        {
            if (IsPlayerPassCity())
            {
                if (CurrentCity == GameConstants.ÑitiesNumber - 1)
                    GameManager.Instance.FinishGame();

                PlayerController.Instance.ResetCityRepeats();

                if (CurrentCity < GameConstants.ÑitiesNumber - 1)
                {
                    CurrentCity++;
                    _positionCurrentCity = PositionCurrentCity();
                    _requiredRepeatsToNextCity = GetRandomRequiredRepeats();
                }
            }
            else
            {
                transform.position = new Vector3(_positionCurrentCity,
                transform.position.y, transform.position.z);
                PlayerController.Instance.IncreaseCityRepeats();
            }
        }
    }

    private bool IsPlayerPassCity() =>
        _requiredRepeatsToNextCity == PlayerController.Instance.CurrentCountRepeats;

    private byte GetRandomRequiredRepeats() =>
        (byte)Random.Range(_minRepeats, _maxRepeats);

    private float PositionCurrentCity() =>
        GameConstants.StartingMovingEnvironmentPosition.x - CurrentCity * _cityWidth;
}
