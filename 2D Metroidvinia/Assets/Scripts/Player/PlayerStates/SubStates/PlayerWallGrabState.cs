using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 _holdPosition;
    public PlayerWallGrabState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
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
        
        HoldPosition();

        if (!GrabInput && IsTouchingWall && IsGrounded)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!GrabInput && IsTouchingWall)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else if (!GrabInput && !IsTouchingWall)
        {
            StateMachine.ChangeState(Player.InAirState);
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
        
        Core.Movement.SetVelocityX(0f);
        Core.Movement.SetVelocityY(0f);
    }
}
