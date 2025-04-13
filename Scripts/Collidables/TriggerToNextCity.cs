using Assets.Scripts.Collidables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToNextCity : MonoBehaviour, ICollidable
{
    public static event Action OnTriggerToNextCity;

    public void CollisionWithPlayer(PlayerController playerController) => 
        OnTriggerToNextCity?.Invoke();
}
