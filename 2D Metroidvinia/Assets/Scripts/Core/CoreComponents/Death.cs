using System;
using UnityEngine;

public class Death : CoreComponent
{
    private ParticleManager ParticleManager => _particleManager ? _particleManager : core.GetCoreComponent(ref _particleManager);
    private ParticleManager _particleManager;
    
    private Stats Stats => _stats ? _stats : core.GetCoreComponent(ref _stats);
    private Stats _stats;
    
    [SerializeField] private GameObject[] deathParticles;
    
    
    public void Die()
    {
        if (deathParticles != null)
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }
        }
        
        core.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
    }
    
    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
        
        Debug.LogError(transform.parent.parent.name + " health is 0!");
    }
}
