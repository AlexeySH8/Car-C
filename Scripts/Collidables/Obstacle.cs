using Assets.Scripts.Collidables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    public int ScoreForObstacle;
    [SerializeField] private Renderer _modelRenderer;
    private ParticleSystem _explosionParticle;
    private float _impulseForce = 5000;
    private float _minReboundForce = 1500;
    private float _maxReboundForce = 5000;
    private float _maxTorque = 1;
    private Rigidbody _rb;

    private void Start()
    {
        _explosionParticle = GetComponentInChildren<ParticleSystem>();
        _rb = GetComponent<Rigidbody>();
    }

    public void CollisionWithPlayer(PlayerController playerController)
    {
        playerController.PlayerStatistics.AddObstaclesDown();
        if (playerController.Powerups.IsPerkIncreaseHPActive)
            playerController.PlayerStatistics.PlayerScore.AddScore(ScoreForObstacle);
        var playerRb = playerController.GetComponent<Rigidbody>();

        if (!playerController.Powerups.IsSpeedPowerupActive)
            ImpulseToPlayer(playerRb);

        VehicleExplosion();

        if (playerController.Powerups.IsSpeedPowerupActive) return;

        playerController.HealthPoints.TakeDamage();
    }

    private void VehicleExplosion()
    {
        AudioManager.Instance.PlayExplosionSFX();
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
