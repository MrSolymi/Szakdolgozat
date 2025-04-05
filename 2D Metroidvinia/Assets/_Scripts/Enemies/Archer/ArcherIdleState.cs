using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Archer
{
    public class ArcherIdleState : EntityIdleState
    {
        private Archer _archer;
        
        public ArcherIdleState(Entity entity, EntityData entityData, string animBoolName, Archer archer) : base(entity, entityData, animBoolName)
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
            
            if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_archer.PlayerDetectedState);
            }
            if (isIdleTimeOver)
            {
                _archer.StateMachine.ChangeState(_archer.MoveState);
            }
        }
    }
}