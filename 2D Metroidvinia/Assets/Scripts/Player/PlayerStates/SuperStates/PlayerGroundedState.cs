using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    
    private bool _jumpInput, _isGrounded, _isTouchingWall, _grabInput, _dashInput;
    
    public PlayerGroundedState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.JumpState.ResetJumps();
        Player.DashState.ResetDash();
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
        _dashInput = Player.InputHandler.DashInput;

        if (_jumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        } else if (!_isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        } else if (_isTouchingWall && _grabInput && !_isGrounded)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (_dashInput && Player.DashState.CheckIfCanDash() && (Core.CollisionSenses.Ground && !Core.CollisionSenses.Wall || !Core.CollisionSenses.Ground && !Core.CollisionSenses.Wall || !Core.CollisionSenses.Ground && Core.CollisionSenses.Wall))
        {
            StateMachine.ChangeState(Player.DashState);
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
