using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityDodgeState : EntityState
    {
        protected bool doCloseRangeAction, isPlayerInMaxAgroRange, isGrounded, isDodgeOver;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        public EntityDodgeState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            isDodgeOver = false;
            
            Movement.SetVelocity(EntityData.dodgeSpeed, EntityData.dodgeAngle, -Movement.FacingDirection);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + EntityData.dodgeTime && isGrounded)
            {
                isDodgeOver = true;
            }
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            doCloseRangeAction = CollisionSenses.PlayerInCloseRangeAction;
            isPlayerInMaxAgroRange = CollisionSenses.PlayerInMaxAgroRange;
            isGrounded = CollisionSenses.Ground;
        }
    }
}