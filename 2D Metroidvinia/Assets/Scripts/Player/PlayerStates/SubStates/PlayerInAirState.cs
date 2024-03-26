using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isGrounded, _jumpInput, _coyoteTime, _isJumping, _jumpInputStop, _isTouchingWall, _grabInput;
    private int _xInput;
    public PlayerInAirState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
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
        
        CheckCoyoteTime();
        _xInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _grabInput = Player.InputHandler.GrabInput;

        CheckJumpMultiplier();

        if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.LandState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        } 
        else if (_isTouchingWall && _grabInput && !_isGrounded)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (_isTouchingWall && _xInput == Core.Movement.FacingDirection && Core.Movement.CurrentVelocity.y <= 0f)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Core.Movement.CheckIfShouldFlip(_xInput);
            Core.Movement.SetVelocityX(PlayerData.movementVelocity * _xInput);
            
            Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
            Player.Animator.SetFloat("xVelocity", Mathf.Abs(Core.Movement.CurrentVelocity.x));
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

    public void StartCoyoteTime() => _coyoteTime = true;
    
    public void SetIsJumping() => _isJumping = true;
    
    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time >= StartTime + PlayerData.coyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseJumps();
        }
    }
    
    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.jumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Core.Movement.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }
    
}
