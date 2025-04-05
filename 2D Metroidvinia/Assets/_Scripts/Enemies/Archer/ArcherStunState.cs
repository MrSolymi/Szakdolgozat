using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Archer
{
    public class ArcherStunState : EntityStunState
    {
        private Archer _archer;
        
        public ArcherStunState(Entity entity, EntityData entityData, string animBoolName, Archer archer) : base(entity, entityData, animBoolName)
        {
            _archer = archer;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsStunTimeOver) return;
            
            if (DoCloseRangeAction)
            {
                StateMachine.ChangeState(_archer.MeleeAttackState);
            }
            else if (IsPlayerInMinAgroRange)
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