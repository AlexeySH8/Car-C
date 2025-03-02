using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToNextCity : MonoBehaviour, ICollidable
{
    public void CollisionWithPlayer(PlayerController playerController)
    {
        TimeManager.Instance.PauseGame();
        UIManager.Instance.ScreenDimmingOn();
        UIManager.Instance.ShowCardSelection();
        ChangeColor.Instance.ChangeSceneMaterial();
        ChangeColor.Instance.ChangeAdsMaterials();
    }
}
