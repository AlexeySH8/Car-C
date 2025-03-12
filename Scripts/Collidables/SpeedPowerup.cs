using Assets.Scripts.Collidables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : BasePowerup, ICollidable
{
    private static float _defaultPowerupDuration = 5f;
    private static float _powerupDuration = _defaultPowerupDuration;
    private float _speedMultiplier = 2f;
    private ParticleSystem _activationParticle;

    void Start()
    {
        _activationParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void CollisionWithPlayer(PlayerController playerController)
    {
        playerController.SpeedTrail_FX.SetActive(true);
        ActivatePowerup(() => ActivatePowerupCourutine(playerController),
            _activationParticle, playerController.Powerups.IsSpeedPowerupActive);
    }

    private IEnumerator ActivatePowerupCourutine(PlayerController playerController)
    {
        SpawnManager.Instance.RemoveElementFromSpawn(gameObject.tag);
        SpawnManager.Instance.SpeedUpSpawnObstacle(_speedMultiplier);
        playerController.Powerups.SpeedPowerupOn();
        MoveLeft.IncreaseSpeed(_speedMultiplier);
        yield return new WaitForSeconds(_powerupDuration);
        SpawnManager.Instance.AddElementToSpawn(gameObject.tag);
        SpawnManager.Instance.ResetSpeedSpawnObstacle();
        playerController.SpeedTrail_FX.SetActive(false);
        playerController.Powerups.SpeedPowerupOff();
        MoveLeft.ResetSpeedToDefault();
        Destroy(gameObject);
    }

    public static void IncreaseEffectDuration(float additionalDuration)
        => _powerupDuration += additionalDuration;

    public static void ResetEffectDuration() => _powerupDuration = _defaultPowerupDuration;
}
