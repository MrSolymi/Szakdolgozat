using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;
using Solymi.Projectiles;
using UnityEngine;

namespace Solymi.Enemies.Archer
{
    public class ArcherRangedAttackState : EntityRangedAttackState
    {
        private Archer _archer;

        public ArcherRangedAttackState(Entity entity, EntityData entityData, string animBoolName, Transform attackPosition, Archer archer) : base(entity, entityData, animBoolName, attackPosition)
        {
            _archer = archer;
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
                    StateMachine.ChangeState(_archer.PlayerDetectedState);
                }
                else
                {
                    StateMachine.ChangeState(_archer.LookForPlayerState);
                }
            }
        }
    }
}