using Solymi.Interfaces;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class Combat : CoreComponent, IDamageable
    {
        private Stats Stats => _stats ? _stats : core.GetCoreComponent(ref _stats);
        private Stats _stats;

        private ParticleManager ParticleManager => _particleManager ? _particleManager : core.GetCoreComponent(ref _particleManager);
        private ParticleManager _particleManager;
    
        [SerializeField] private GameObject hitParticles;
    
        public void Damage(float amount)
        {
            Debug.LogError(core.transform.parent.name + " damaged!");
            Stats.DecreaseHealth(amount);
            ParticleManager.StartParticlesRandomRotation(hitParticles);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }
}
