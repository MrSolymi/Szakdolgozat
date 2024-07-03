using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Archer
{
    public class ArcherMoveState : EntityMoveState
    {
        private Archer _archer;
        
        public ArcherMoveState(Entity entity, EntityData entityData, string animBoolName, Archer archer) : base(entity, entityData, animBoolName)
        {
            _archer = archer;
        }

        public override void Enter()
        {
            base.Enter();
            
            Movement.SetVelocityX(EntityData.movementSpeed * Movement.FacingDirection);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_archer.PlayerDetectedState);
            }
            else if (isDetectingWall || !isDetectingLedge)
            {
                _archer.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_archer.IdleState);
            }
        }
    }
}