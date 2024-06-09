using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityStunState : EntityState
    {
        protected bool IsGrounded ,IsStunTimeOver, IsMovementStopped, IsPlayerInMinAgroRange, DoCloseRangeAction;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        private KnockBackReceiver KnockBackReceiver => _knockBackReceiver ? _knockBackReceiver : Core.GetCoreComponent(ref _knockBackReceiver);
        private KnockBackReceiver _knockBackReceiver;
        
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        public EntityStunState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            IsStunTimeOver = false;
            IsMovementStopped = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + EntityData.stunDuration)
            {
                IsStunTimeOver = true;
            }

            if (IsGrounded && Time.time >= StartTime + KnockBackReceiver.knockBackMaxDuration && !IsMovementStopped)
            {
                Movement.SetVelocityZero();
                IsMovementStopped = true;
            }
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            IsGrounded = CollisionSenses.Ground;
            DoCloseRangeAction = CollisionSenses.PlayerInCloseRangeAction;
            IsPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
        }
    }
}