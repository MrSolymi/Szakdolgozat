using System;
using Solymi.Core.Stats;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class Stats : CoreComponent
    {
        [field: SerializeField] public Stat Health { get; private set; }
        //[field: Header("Poise")]
        [field: SerializeField] public Stat Poise { get; private set; }
        [SerializeField] private float poiseRecoveryRate;
        
        public bool IsDamaged { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Health.Initialize();
            Poise.Initialize();
            
            IsDamaged = false;
        }

        private void Update()
        {
            if (Poise.CurrentValue.Equals(Poise.MaxValue)) return;
            
            Poise.Increase(poiseRecoveryRate * Time.deltaTime);
        }

        public void SetIsDamaged(bool value) => IsDamaged = value;
    }
}
