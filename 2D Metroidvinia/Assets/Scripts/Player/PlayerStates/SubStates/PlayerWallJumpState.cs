using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int _wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
        IsAbilityDone = false;

        Player.InputHandler.UseJumpInput();
        Player.JumpState.ResetJumps();
        Core.Movement.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, _wallJumpDirection);
        Core.Movement.CheckIfShouldFlip(_wallJumpDirection);
        Player.JumpState.DecreaseJumps();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
        Player.Animator.SetFloat("xVelocity", Mathf.Abs(Core.Movement.CurrentVelocity.x));

        if (Time.time >= StartTime + PlayerData.wallJumpTime)
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
            _wallJumpDirection = -Core.Movement.FacingDirection;
        }
        else
        {
            _wallJumpDirection = Core.Movement.FacingDirection;
        }
    }
}
