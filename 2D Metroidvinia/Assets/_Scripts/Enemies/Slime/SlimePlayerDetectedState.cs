using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Slime
{
    public class SlimePlayerDetectedState : EntityPlayerDetectedState
    {
        private Slime _slime;
        public SlimePlayerDetectedState(Entity entity, EntityData entityData, string animBoolName, Slime slime) : base(entity, entityData, animBoolName)
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

            if (!isDetectingLedge)
            {
                Movement.Flip();
                StateMachine.ChangeState(_slime.IdleState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                StateMachine.ChangeState(_slime.LookForPlayerState);
            }
            else
            {
                StateMachine.ChangeState(_slime.JumpPrepState);
            }
        }
    }
}