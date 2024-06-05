using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Enemy1
{
    public class Enemy1PlayerDetectedState : EntityPlayerDetectedState
    {
        private Enemy1 _enemy;
        
        public Enemy1PlayerDetectedState(Entity entity, EntityData entityData, string animBoolName, Enemy1 enemy) : base(entity, entityData, animBoolName)
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

            if (doCloseRangeAction)
            {
                StateMachine.ChangeState(_enemy.MeleeAttackState);
            }
            else if (doLongRangeAction)
            {
                StateMachine.ChangeState(_enemy.ChargeState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                StateMachine.ChangeState(_enemy.LookForPlayerState);
            }
            else if (!isDetectingLedge)
            {
                _enemy.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_enemy.IdleState);
            }
        }
    }
}