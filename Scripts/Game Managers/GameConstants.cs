using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{
    public static readonly float[] Roads = { -24.5f, 0.0f, 24.5f };
    public static readonly byte ÑitySegmentCount = 4;
    public static readonly byte ÑitiesNumber = 3;
    public static readonly Vector3 StartingPlayerCarPosition = new Vector3(-293.4f, 4.55f, 24.5f);
    public static readonly Vector3 StartingMovingEnvironmentPosition = new Vector3(-225, 0, 0); // 225
    public static readonly Vector3 PositionToTriggerToNextCity1 = new Vector3(1406, -93, -390);
    public static readonly Vector3 PositionToTriggerToNextCity2 = new Vector3(3479, -93, -390);
}
