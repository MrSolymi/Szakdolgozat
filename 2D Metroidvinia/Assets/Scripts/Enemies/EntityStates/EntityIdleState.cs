using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityIdleState : EntityState
    {
        protected bool flipAfterIdle, isIdleTimeOver, isPlayerInMinAgroRange, isDamaged;
    
        protected float idleTime;
    
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected Stats Stats => _stats ? _stats : Core.GetCoreComponent(ref _stats);
        private Stats _stats;
        public EntityIdleState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            isIdleTimeOver = false;
            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();

            if (flipAfterIdle)
            {
                Movement.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + idleTime)
            {
                isIdleTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            isPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
            isDamaged = Stats.IsDamaged;
        }
    
        public void SetFlipAfterIdle(bool flip)
        {
            flipAfterIdle = flip;
        }
    
        private void SetRandomIdleTime()
        {
            idleTime = Random.Range(EntityData.minIdleTime, EntityData.maxIdleTime);
        }
    }
}
