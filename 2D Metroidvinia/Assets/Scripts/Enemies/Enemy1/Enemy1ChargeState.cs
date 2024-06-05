using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Enemy1
{
    public class Enemy1ChargeState : EntityChargeState
    {
        private Enemy1 _enemy;
        
        public Enemy1ChargeState(Entity entity, EntityData entityData, string animBoolName, Enemy1 enemy) : base(entity, entityData, animBoolName)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            
            Movement.SetVelocityX(EntityData.chargeSpeed * Movement.FacingDirection);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (isChargeTimeOver)
            {
                if (isPlayerInMinAgroRange)
                {
                    StateMachine.ChangeState(_enemy.ChargeState);
                }
                else
                {
                    StateMachine.ChangeState(_enemy.LookForPlayerState);
                }
            }
            else if (doCloseRangeAction)
            {
                isChargeTimeOver = true;
                StateMachine.ChangeState(_enemy.MeleeAttackState);
            }
            else if (!isDetectingLedge || isDetectingWall)
            {
                //_enemy.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_enemy.LookForPlayerState);
            }
            
        }
    }
}