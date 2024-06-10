using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Slime
{
    public class SlimeIdleState : EntityIdleState
    {
        private Slime _slime;
        public SlimeIdleState(Entity entity, EntityData entityData, string animBoolName, Slime slime) : base(entity, entityData, animBoolName)
        {
            _slime = slime;
        }

        public override void Enter()
        {
            base.Enter();
            
            Movement.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isDamaged)
            {
                Stats.SetIsDamaged(false);
                StateMachine.ChangeState(_slime.LookForPlayerState);
            }
            else if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_slime.PlayerDetectedState);
            }
        }
    }
}