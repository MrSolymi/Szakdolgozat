using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityMeleeAttackState : EntityAttackState
    {
        public EntityMeleeAttackState(Entity entity, EntityData entityData, string animBoolName, Transform attackPosition) : base(entity, entityData, animBoolName, attackPosition)
        {
        }
        
        public override void TriggerAttack()
        {
            base.TriggerAttack();
            
            var detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, EntityData.meleeAttackRadius, CollisionSenses.WhatIsPlayer);

            foreach (var item in detectedObjects)
            {
                
                // if (item.TryGetComponent(out IDamageable damageable))
                // {
                //     Debug.LogError(damageable);
                //     damageable.Damage(EntityData.meleeAttackDamage);
                // }

                if (item.GetComponentInChildren<DamageReceiver>().TryGetComponent(out IDamageable damageable))
                {
                    //Debug.LogError(damageable);
                    damageable.Damage(EntityData.meleeAttackDamage);
                }

                if (item.GetComponentInChildren<KnockBackReceiver>().TryGetComponent(out IKnockBackable knockBackable))
                {
                    knockBackable.KnockBack(EntityData.knockBackAngle, EntityData.knockBackStrength, Movement.FacingDirection);
                }
            }
        }

        
    }
}