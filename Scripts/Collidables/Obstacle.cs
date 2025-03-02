using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    [SerializeField] private Renderer _modelRenderer;
    private ParticleSystem _explosionParticle;
    private float _impulseForce;
    private float _minReboundForce;
    private float _maxReboundForce;
    private float _maxTorque;
    private Rigidbody _rb;

    private void Start()
    {
        _impulseForce = 5000;
        _minReboundForce = 1500;
        _maxReboundForce = 5000;
        _maxTorque = 1;
        _explosionParticle = GetComponentInChildren<ParticleSystem>();
        _rb = GetComponent<Rigidbody>();
    }

    public void CollisionWithPlayer(PlayerController playerController)
    {
        var playerRb = playerController.GetComponent<Rigidbody>();
        if (!playerController.IsSpeedPowerupActive)
            ImpulseToPlayer(playerRb);
        VehicleExplosion();
        if (playerController.IsSpeedPowerupActive) return;

        playerController.HealthPoints.TakeDamage();
    }

    private void VehicleExplosion()
    {
        _explosionParticle.Play();
        ImpulseToObstacle();
        _modelRenderer.materials = ChangeColor.Instance
            .ChangeMaterialAfterExplosion(_modelRenderer.materials.Length);
    }

    private void ImpulseToObstacle()
    {
        Vector3 randomTorque = new Vector3(
        Random.Range(-_maxTorque, _maxTorque),
        Random.Range(-_maxTorque, _maxTorque),
        Random.Range(-_maxTorque, _maxTorque));

        _rb.AddForce(Vector3.up * Random.Range(_minReboundForce,
            _maxReboundForce), ForceMode.Impulse);

        _rb.AddTorque(randomTorque * Random.Range(_minReboundForce,
            _maxReboundForce), ForceMode.Impulse);
    }

    private void ImpulseToPlayer(Rigidbody playerRb) =>
        playerRb.AddForce(Vector3.left * _impulseForce, ForceMode.Impulse);
}
