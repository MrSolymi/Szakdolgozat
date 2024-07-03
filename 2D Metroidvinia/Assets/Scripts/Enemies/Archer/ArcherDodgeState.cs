using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Archer
{
    public class ArcherDodgeState : EntityDodgeState
    {
        private Archer _archer;
        
        public ArcherDodgeState(Entity entity, EntityData entityData, string animBoolName, Archer archer) : base(entity, entityData, animBoolName)
        {
            _archer = archer;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isDodgeOver)
            {
                if (isPlayerInMaxAgroRange && doCloseRangeAction)
                {
                    StateMachine.ChangeState(_archer.MeleeAttackState);
                }
                else if (isPlayerInMaxAgroRange && !doCloseRangeAction)
                {
                    StateMachine.ChangeState(_archer.RangedAttackState);
                }
                else if (!isPlayerInMaxAgroRange)
                {
                    StateMachine.ChangeState(_archer.LookForPlayerState);
                }
            }
        }
    }
}