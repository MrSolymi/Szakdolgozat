using Solymi.Player.Data;
using Solymi.Player.PlayerStates.SuperStates;

namespace Solymi.Player.PlayerStates.SubStates
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        public PlayerWallSlideState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsExitingState)
            {
                Movement.SetVelocityY(-PlayerData.wallSlideVelocity);
                    
                if (GrabInput)
                {
                    StateMachine.ChangeState(Player.WallGrabState);
                } else if (DashInput)
                {
                    StateMachine.ChangeState(Player.DashState);
                }
            }
        
            // else if (JumpInput)
            // {
            //     Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            //     StateMachine.ChangeState(Player.WallJumpState);
            // }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}
