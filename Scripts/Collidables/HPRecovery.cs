using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRecovery : BasePowerup, ICollidable
{
    private ParticleSystem _activationParticle;

    void Start()
    {
        _activationParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void CollisionWithPlayer(PlayerController playerController)
    {
        _activationParticle.Play();
        RemoveElement();
        playerController.HealthPoints.TakeHP();
        StartCoroutine(DestroyAfterEffect(_activationParticle.main.duration + 0.5f));
    }
}
