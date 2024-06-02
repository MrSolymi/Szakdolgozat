using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityAttackState : EntityState
    {
        protected Transform attackPosition;
        protected bool isAnimationFinished, isPlayerInMinAgroRange;
        
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        protected CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        public EntityAttackState(Entity entity, EntityData entityData, string animBoolName, Transform attackPosition) : base(entity, entityData, animBoolName)
        {
            this.attackPosition = attackPosition;
        }
       

        public override void Enter()
        {
            base.Enter();
            
            isAnimationFinished = false;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            isPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
        }

        public virtual void TriggerAttack()
        {
            //Debug.Log("Attack Triggered");
        }
        
        public virtual void FinishAttack()
        {
            //Debug.Log("Attack Finished");
            
            isAnimationFinished = true;
        }
    }
}