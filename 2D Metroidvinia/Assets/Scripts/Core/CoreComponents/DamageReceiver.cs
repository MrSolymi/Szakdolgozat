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
            _stats.DecreaseHealth(amount);
            _particleManager.StartParticlesRandomRotation(hitParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            _stats = core.GetCoreComponent<Stats>();
            _particleManager = core.GetCoreComponent<ParticleManager>();
            
            //_stats = new CoreComponent<Stats>(core);
            //_particleManager = new CoreComponent<ParticleManager>(core);
        }
    }
}