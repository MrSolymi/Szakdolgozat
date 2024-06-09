using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class Death : CoreComponent
    {
        private ParticleManager _particleManager;
        private Stats _stats;
    
        [SerializeField] private GameObject[] deathParticles;

        protected override void Awake()
        {
            base.Awake();

            _particleManager = core.GetCoreComponent<ParticleManager>();
            _stats = core.GetCoreComponent<Stats>();
        }

        public void Die()
        {
            if (deathParticles != null)
            {
                foreach (var particle in deathParticles)
                {
                    _particleManager.StartParticles(particle);
                }
            }
        
            core.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _stats.Health.OnCurrentValueZero += Die;
        }
    
        private void OnDisable()
        {
            _stats.Health.OnCurrentValueZero -= Die;
        }
    }
}
