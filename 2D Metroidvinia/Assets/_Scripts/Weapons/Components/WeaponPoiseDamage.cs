using Solymi.Interfaces;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class WeaponPoiseDamage : WeaponComponent<WeaponPoiseDamageData, AttackPoiseDamage>
    {
        private ActionHitBox _hitBox;

        protected override void Start()
        {
            base.Start();
            
            _hitBox = GetComponent<ActionHitBox>();
            
            _hitBox.OnDetectedColliders += HandleDetectedColliders;
        }

        private void HandleDetectedColliders(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
                {
                    poiseDamageable.PoiseDamage(currentAttackData.PoiseDamage);
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _hitBox.OnDetectedColliders -= HandleDetectedColliders;
        }
    }
}