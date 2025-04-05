using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;
using UnityEngine;

namespace Solymi.Enemies.Enemy1
{
    public class Enemy1MeleeAttackState : EntityMeleeAttackState
    {
        private Enemy1 _enemy;

        public Enemy1MeleeAttackState(Entity entity, EntityData entityData, string animBoolName, Transform attackPosition, Enemy1 enemy) : base(entity, entityData, animBoolName, attackPosition)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            Movement.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isAnimationFinished)
            {
                if (isPlayerInMinAgroRange)
                {
                    StateMachine.ChangeState(_enemy.PlayerDetectedState);
                }
                else
                {
                    StateMachine.ChangeState(_enemy.LookForPlayerState);
                }
            }
        }
    }
}