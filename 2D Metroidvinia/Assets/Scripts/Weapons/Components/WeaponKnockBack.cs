using Solymi.Core.CoreComponents;
using Solymi.Interfaces;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class WeaponKnockBack : WeaponComponent<WeaponKnockBackData, AttackKnockBack>
    {
        private ActionHitBox _hitBox;
        
        private Movement _movement;

        protected override void Start()
        {
            base.Start();
            
            _hitBox = GetComponent<ActionHitBox>();
            
            _hitBox.OnDetectedColliders += HandleDetectedColliders;

            _movement = Core.GetCoreComponent<Movement>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _hitBox.OnDetectedColliders -= HandleDetectedColliders;
        }

        private void HandleDetectedColliders(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IKnockBackable knockBackable))
                {
                    //Debug.LogError(item.gameObject.name);
                    knockBackable.KnockBack(currentAttackData.KnockBackAngle, currentAttackData.KnockBackStrength, _movement.FacingDirection);
                }
            }
        }
    }
}