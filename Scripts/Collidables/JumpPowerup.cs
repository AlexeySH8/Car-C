using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerup : BasePowerup, ICollidable
{
    private float powerupDuration = 7;
    private float jumpMultiplier = 1.5f;
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
        var playerJump = playerController.GetComponent<PlayerJump>();
        var defaultJumpForce = playerJump.JumpForce;
        playerController.JumpPowerupOn();
        playerJump.IncreaseJumpForce(jumpMultiplier);
        yield return new WaitForSeconds(powerupDuration);
        playerController.Jump_FX.SetActive(false);
        playerController.JumpPowerupOff();
        playerJump.ResetJumpForce();
        Destroy(gameObject);
    }
}

