using Assets.Scripts.Collidables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : BasePowerup, ICollidable
{
    private float powerupDuration = 5f;
    private float speedMultiplier = 2f;
    private ParticleSystem _activationParticle;

    void Start()
    {
        _activationParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void CollisionWithPlayer(PlayerController playerController)
    {
        playerController.SpeedTrail_FX.SetActive(true);
        ActivatePowerup(() => ActivatePowerupCourutine(playerController),
            _activationParticle, playerController.IsSpeedPowerupActive);
    }

    private IEnumerator ActivatePowerupCourutine(PlayerController playerController)
    {
        SpawnManager.Instance.SpeedUpSpawnObstacle();
        playerController.SpeedPowerupOn();
        MoveLeft.IncreaseSpeed(speedMultiplier);
        yield return new WaitForSeconds(powerupDuration);
        SpawnManager.Instance.ResetSpeedSpawnObstacle();
        playerController.SpeedTrail_FX.SetActive(false);
        playerController.SpeedPowerupOff();
        MoveLeft.ResetSpeedToDefault();
        Destroy(gameObject);
    }
}
