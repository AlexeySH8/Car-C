using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToNextCity : MonoBehaviour, ICollidable
{
    public void CollisionWithPlayer(PlayerController playerController)
    {
        ChangeColor.Instance.ChangeSceneMaterial();
        ChangeColor.Instance.ChangeAdsMaterials();
        Debug.Log("TriggerToNextCity");
    }
}
