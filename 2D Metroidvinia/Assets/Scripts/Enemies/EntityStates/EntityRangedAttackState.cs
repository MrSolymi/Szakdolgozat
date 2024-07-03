using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Projectiles;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityRangedAttackState : EntityAttackState
    {
        protected GameObject projectile;
        protected ArcherProjectile archerProjectile;
        
        public EntityRangedAttackState(Entity entity, EntityData entityData, string animBoolName, Transform attackPosition) : base(entity, entityData, animBoolName, attackPosition)
        {
        }

        public override void TriggerAttack()
        {
            base.TriggerAttack();
            
            var projectiles = GameObject.Find("Projectiles");

            projectile = Object.Instantiate(EntityData.projectile, attackPosition.position, attackPosition.rotation, projectiles.transform);
            
            archerProjectile = projectile.GetComponent<ArcherProjectile>();
            archerProjectile.FireProjectile(EntityData.projectileSpeed, EntityData.projectileTravelDistance, EntityData.projectileDamage);
        }

        
    }
}