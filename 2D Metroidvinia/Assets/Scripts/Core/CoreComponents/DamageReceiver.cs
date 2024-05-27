using Solymi.Interfaces;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject hitParticles;

        private CoreComponent<Stats> _stats;
        private CoreComponent<ParticleManager> _particleManager;
        
        public void Damage(float amount)
        {
            Debug.LogError(core.transform.parent.name + " damaged!");
            _stats.Component.DecreaseHealth(amount);
            _particleManager.Component.StartParticlesRandomRotation(hitParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            _stats = new CoreComponent<Stats>(core);
            _particleManager = new CoreComponent<ParticleManager>(core);
        }
    }
}