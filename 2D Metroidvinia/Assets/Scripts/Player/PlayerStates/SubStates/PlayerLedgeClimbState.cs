using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _detectedPosition, _cornerPosition, _startPosition, _stopPosition, _workspace;
    private bool _isHanging, _isClimbing, _jumpInput;
    private int _xInput, _yInput;
    
    public PlayerLedgeClimbState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }
    
    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;

    public override void Enter()
    {
        base.Enter();
        
        Core.Movement.SetVelocityZero();
        Player.transform.position = _detectedPosition;
        _cornerPosition = GetCornerPosition();

        _startPosition.Set(_cornerPosition.x - (Core.Movement.FacingDirection * PlayerData.ledgeClimbStartOffset.x), _cornerPosition.y - PlayerData.ledgeClimbStartOffset.y);
        _stopPosition.Set(_cornerPosition.x + (Core.Movement.FacingDirection * PlayerData.ledgeClimbStopOffset.x), _cornerPosition.y + PlayerData.ledgeClimbStopOffset.y);

        Player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();
        
        _isHanging = false;
        if (_isClimbing)
        {
            _isClimbing = false;
            Player.transform.position = _stopPosition;
        }
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else
        {
            Core.Movement.SetVelocityZero();
            Player.transform.position = _startPosition;
        
            _xInput = Player.InputHandler.NormalizedInputX;
            _yInput = Player.InputHandler.NormalizedInputY;
            _jumpInput = Player.InputHandler.JumpInput;

            if (_xInput == Core.Movement.FacingDirection && _isHanging && !_isClimbing)
            {
                _isClimbing = true;
                Player.Animator.SetBool("climbLedge", true);
            } else if (_yInput == -1 && _isHanging && !_isClimbing)
            {
                StateMachine.ChangeState(Player.InAirState);
            }
            else if (_jumpInput && _isHanging && !_isClimbing)
            {
                Player.WallJumpState.DetermineWallJumpDirection(true);
                StateMachine.ChangeState(Player.WallJumpState);
            }
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

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        
        _isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        Player.Animator.SetBool("climbLedge", false);
    }
    
    private Vector2 GetCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(Core.CollisionSenses.WallCheck.position, 
            Vector2.right * Core.Movement.FacingDirection, 
            Core.CollisionSenses.WallCheckDistance, 
            Core.CollisionSenses.WhatIsGround);
        float xDistance = xHit.distance;
       
        _workspace.Set(xDistance * Core.Movement.FacingDirection, 0f);
       
        RaycastHit2D yHit = Physics2D.Raycast(Core.CollisionSenses.LedgeCheckHorizontal.position + (Vector3)_workspace, 
            Vector2.down, 
            Core.CollisionSenses.LedgeCheckHorizontal.position.y - Core.CollisionSenses.WallCheck.position.y, 
            Core.CollisionSenses.WhatIsGround);
        float yDistance = yHit.distance;
       
        _workspace.Set(Core.CollisionSenses.WallCheck.position.x + (xDistance * Core.Movement.FacingDirection), Core.CollisionSenses.LedgeCheckHorizontal.position.y - yDistance);
       
        return _workspace;
    }
}
