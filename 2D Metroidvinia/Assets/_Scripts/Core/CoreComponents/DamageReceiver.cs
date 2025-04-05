using Solymi.Interfaces;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject hitParticles;

        private Stats _stats;
        private ParticleManager _particleManager;
        
        public void Damage(float amount)
        {
            //Debug.LogError(core.transform.parent.name + " damaged!");
            
            _stats.Health.Decrease(amount);
            _particleManager.StartParticlesRandomRotation(hitParticles);
            
            _stats.SetIsDamaged(true);
        }

        protected override void Awake()
        {
            base.Awake();

            _stats = core.GetCoreComponent<Stats>();
            _particleManager = core.GetCoreComponent<ParticleManager>();
        }
    }
}