using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerup : MonoBehaviour
{
    protected virtual void ActivatePowerup(Func<IEnumerator> activatePowerupCourutine,
        ParticleSystem activationParticle, bool isEffectActivated)
    {
        GetComponent<DestroyObject>().enabled = false;
        activationParticle.Play();
        RemoveElement();
        if (isEffectActivated)
        {
            StartCoroutine(DestroyAfterEffect(activationParticle.main.duration + 0.5f));
            return;
        }
        StartCoroutine(activatePowerupCourutine());        
    }

    protected void RemoveElement()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
            mesh.enabled = false;
        foreach (Collider collider in GetComponentsInChildren<Collider>())
            collider.enabled = false;
    }

    protected IEnumerator DestroyAfterEffect(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        Destroy(gameObject);
    }
}
