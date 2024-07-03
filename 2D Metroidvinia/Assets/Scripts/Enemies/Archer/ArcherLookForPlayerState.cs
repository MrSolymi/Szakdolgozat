using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Archer
{
    public class ArcherLookForPlayerState : EntityLookForPlayerState
    {
        private Archer _archer;
        
        public ArcherLookForPlayerState(Entity entity, EntityData entityData, string animBoolName, Archer archer) : base(entity, entityData, animBoolName)
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
            else if (isTurningTimesOver)
            {
                StateMachine.ChangeState(_archer.MoveState);
            }
        }
    }
}