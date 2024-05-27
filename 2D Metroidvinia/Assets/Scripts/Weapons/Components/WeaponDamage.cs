using Solymi.Interfaces;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class WeaponDamage : WeaponComponent<WeaponDamageData, AttackDamage>
    {
        private ActionHitBox _hitBox;
        
        private void HandleDetectedColliders(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(currentAttackData.DamageAmount);
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            _hitBox = GetComponent<ActionHitBox>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _hitBox.OnDetectedColliders += HandleDetectedColliders;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _hitBox.OnDetectedColliders -= HandleDetectedColliders;
        }
    }
}