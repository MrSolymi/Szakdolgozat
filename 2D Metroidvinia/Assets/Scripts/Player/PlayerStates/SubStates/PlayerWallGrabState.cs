using Solymi.Player.Data;
using Solymi.Player.PlayerStates.SuperStates;
using UnityEngine;

namespace Solymi.Player.PlayerStates.SubStates
{
    public class PlayerWallGrabState : PlayerTouchingWallState
    {
        private Vector2 _holdPosition;
        public PlayerWallGrabState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        
            _holdPosition = Player.transform.position;
            HoldPosition();
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
                HoldPosition();
            
                if (!GrabInput)
                {
                    StateMachine.ChangeState(Player.WallSlideState);
                }
                else if (DashInput)
                {
                    StateMachine.ChangeState(Player.DashState);
                }
                // if (GrabInput && JumpInput)
                // {   
                //     Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
                //     StateMachine.ChangeState(Player.WallJumpState);
                // }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        private void HoldPosition()
        {
            Player.transform.position = _holdPosition;
        
            Movement.SetVelocityX(0f);
            Movement.SetVelocityY(0f);
        }
    }
}
