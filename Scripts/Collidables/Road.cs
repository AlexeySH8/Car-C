using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, ICollidable
{
    public void CollisionWithPlayer(PlayerController playerController)
    {
        playerController.SetOnRoad(true);
    }
}
