using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    
    private bool _jumpInput, _isGrounded, _isTouchingWall, _grabInput;
    
    public PlayerGroundedState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.JumpState.ResetJumps();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        XInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _grabInput = Player.InputHandler.GrabInput;

        if (_jumpInput && Player.JumpState.CanJump())
        {
            Player.InputHandler.UseJumpInput();
            StateMachine.ChangeState(Player.JumpState);
        } else if (!_isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        } else if (_isTouchingWall && _grabInput && !_isGrounded)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        _isGrounded = Player.Core.CollisionSenses.Ground;
        _isTouchingWall = Player.Core.CollisionSenses.Wall;
    }

    
}
