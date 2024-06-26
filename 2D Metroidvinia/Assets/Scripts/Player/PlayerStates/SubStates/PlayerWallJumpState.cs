using Solymi.Player.Data;
using Solymi.Player.PlayerStates.SuperStates;
using UnityEngine;

namespace Solymi.Player.PlayerStates.SubStates
{
    public class PlayerWallJumpState : PlayerAbilityState
    {
        private int _wallJumpDirection;
        public PlayerWallJumpState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        
            IsAbilityDone = false;

            Player.InputHandler.UseJumpInput();
            Player.JumpState.ResetJumps();
            Movement.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, _wallJumpDirection);
            Movement.CheckIfShouldFlip(_wallJumpDirection);
            Player.JumpState.DecreaseJumps();
        
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            Player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            Player.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

            if (Time.time >= StartTime + PlayerData.wallJumpTime || CollisionSenses.Wall)
            {
                IsAbilityDone = true;
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

        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            if (isTouchingWall)
            {
                _wallJumpDirection = -Movement.FacingDirection;
            }
            else
            {
                _wallJumpDirection = Movement.FacingDirection;
            }
        }
    }
}
