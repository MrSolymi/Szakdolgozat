using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Enemy1
{
    public class Enemy1StunState : EntityStunState
    {
        private Enemy1 _enemy;
        
        public Enemy1StunState(Entity entity, EntityData entityData, string animBoolName, Enemy1 enemy) : base(entity, entityData, animBoolName)
        {
            _enemy = enemy;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsStunTimeOver) return;
            
            if (DoCloseRangeAction)
            {
                StateMachine.ChangeState(_enemy.MeleeAttackState);
            }
            else if (IsPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.ChargeState);
            }
            else
            {
                StateMachine.ChangeState(_enemy.LookForPlayerState);
            }
        }
    }
}