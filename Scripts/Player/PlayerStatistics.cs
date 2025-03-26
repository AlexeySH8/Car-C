using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public PlayerScore PlayerScore;
    public int DistanceTraveled { get; private set; }
    public int ObstaclesDown { get; private set; }

    private void Awake()
    {
        PlayerScore = GetComponent<PlayerScore>();
    }

    public void ResetPlayerStatistics()
    {
        PlayerScore.ResetCurrentScoreAmount();
        DistanceTraveled = 0;
        ObstaclesDown = 0;
    }

    public void CountDistanceTraveled(int segmentWidth) => DistanceTraveled += segmentWidth;

    public void AddObstaclesDown() => ObstaclesDown++;
}
