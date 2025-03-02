using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerup : BasePowerup, ICollidable
{
    private static float _defaultPowerupDuration = 7f;
    private static float _powerupDuration = _defaultPowerupDuration;
    private float _jumpMultiplier = 1.5f;
    private ParticleSystem _activationParticle;

    void Start()
    {
        _activationParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void CollisionWithPlayer(PlayerController playerController)
    {
        playerController.Jump_FX.SetActive(true);
        ActivatePowerup(() => ActivatePowerupCourutine(playerController),
            _activationParticle, playerController.IsJumpPowerupActive);
    }

    private IEnumerator ActivatePowerupCourutine(PlayerController playerController)
    {
        Debug.Log("Jump: " + _powerupDuration);
        var playerJump = playerController.GetComponent<PlayerJump>();
        var defaultJumpForce = playerJump.JumpForce;
        playerController.JumpPowerupOn();
        playerJump.IncreaseJumpForce(_jumpMultiplier);
        yield return new WaitForSeconds(_powerupDuration);
        playerController.Jump_FX.SetActive(false);
        playerController.JumpPowerupOff();
        playerJump.ResetJumpForce();
        Destroy(gameObject);
    }

    public static void IncreaseSpeedDuration(float additionalDuration)
        => _powerupDuration += additionalDuration;

    public static void ResetSpeedDuration() => _powerupDuration = _defaultPowerupDuration;
}

