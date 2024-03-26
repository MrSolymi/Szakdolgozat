using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool IsGrounded, IsTouchingWall, GrabInput;
    protected int XInput;
    public PlayerTouchingWallState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
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
        
        XInput = Player.InputHandler.NormalizedInputX;
        GrabInput = Player.InputHandler.GrabInput;

        if (IsGrounded && !GrabInput)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!IsTouchingWall || (XInput != Core.Movement.FacingDirection && !GrabInput))
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
        
        IsGrounded = Core.CollisionSenses.Ground;
        IsTouchingWall = Core.CollisionSenses.Wall;
    }
}
