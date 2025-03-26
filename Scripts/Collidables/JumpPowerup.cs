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
        AudioManager.Instance.PlayJumpPowerupSFX();
        playerController.Jump_FX.SetActive(true);
        ActivatePowerup(() => ActivatePowerupCourutine(playerController),
            _activationParticle, playerController.Powerups.IsJumpPowerupActive);
    }

    private IEnumerator ActivatePowerupCourutine(PlayerController playerController)
    {
        var playerJump = playerController.GetComponent<PlayerJump>();
        var defaultJumpForce = playerJump.JumpForce;
        playerController.Powerups.JumpPowerupOn();
        playerJump.IncreaseJumpForce(_jumpMultiplier);
        SpawnManager.Instance.RemoveElementFromSpawn(gameObject.tag);
        yield return new WaitForSeconds(_powerupDuration);
        SpawnManager.Instance.AddElementToSpawn(gameObject.tag);
        playerController.Jump_FX.SetActive(false);
        playerController.Powerups.JumpPowerupOff();
        playerJump.ResetJumpForce();
        Destroy(gameObject);
    }

    public static void IncreaseEffectDuration(float additionalDuration)
        => _powerupDuration += additionalDuration;

    public static void ResetEffectDuration() => _powerupDuration = _defaultPowerupDuration;
}

