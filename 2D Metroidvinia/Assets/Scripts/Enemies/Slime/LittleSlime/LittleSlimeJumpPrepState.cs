using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Slime.LittleSlime
{
    public class LittleSlimeJumpPrepState : EntityJumpPrepState
    {
        private LittleSlime _slime;
        public LittleSlimeJumpPrepState(Entity entity, EntityData entityData, string animBoolName, LittleSlime slime) : base(entity, entityData, animBoolName)
        {
            _slime = slime;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            //Movement.SetVelocityX(0f);
            Movement.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsExitingState && IsAnimationFinished && isGrounded)
            {
                StateMachine.ChangeState(_slime.JumpState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}