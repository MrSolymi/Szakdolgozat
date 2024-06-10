using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Slime.LittleSlime
{
    public class LittleSlimeJumpLandState : EntityJumpLandState
    {
        private LittleSlime _slime;
        public LittleSlimeJumpLandState(Entity entity, EntityData entityData, string animBoolName, LittleSlime slime) : base(entity, entityData, animBoolName)
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

            if (!IsExitingState && IsAnimationFinished)
            {
                if (!isPlayerInMaxAgroRange)
                {
                    StateMachine.ChangeState(_slime.LookForPlayerState);
                } 
                else if (isPlayerInMinAgroRange)
                {
                    StateMachine.ChangeState(_slime.PlayerDetectedState);
                }
            }
        }
    }
}