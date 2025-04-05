using Solymi.Interfaces;

namespace Solymi.Core.CoreComponents
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {
        private Stats _stats;
        public void PoiseDamage(float poiseDamage)
        {
            _stats.Poise.Decrease(poiseDamage);
        }

        protected override void Awake()
        {
            base.Awake();
            
            _stats = core.GetCoreComponent<Stats>();
        }
    }
}