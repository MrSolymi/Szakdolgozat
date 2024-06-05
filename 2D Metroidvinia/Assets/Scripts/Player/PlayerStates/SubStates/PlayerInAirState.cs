using Solymi.Core.CoreComponents;
using Solymi.Enums;
using Solymi.Player.Data;
using Solymi.Player.PlayerStateMachine;
using UnityEngine;

namespace Solymi.Player.PlayerStates.SubStates
{
    public class PlayerInAirState : PlayerState
    {
        private bool _isGrounded, _isJumping, _isTouchingWall, _isTouchingWallBackwards, 
            _oldIsTouchingWall, _oldIsTouchingWallBackwards, _isTouchingLedge;
        private bool _jumpInput, _jumpInputStop, _grabInput, _dashInput;
        private bool _coyoteTime, _wallJumpCoyoteTime;
    
        private int _xInput;
        private float _startWallJumpCoyoteTime;
    
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        protected CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        public PlayerInAirState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        
            _isTouchingWall = false;
            _isTouchingWallBackwards = false;
            _oldIsTouchingWall = false;
            _oldIsTouchingWallBackwards = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            CheckCoyoteTime();
            CheckWallJumpCoyoteTime();
        
            _xInput = Player.InputHandler.NormalizedInputX;
            _jumpInput = Player.InputHandler.JumpInput;
            _jumpInputStop = Player.InputHandler.JumpInputStop;
            _grabInput = Player.InputHandler.GrabInput;
            _dashInput = Player.InputHandler.DashInput;

            CheckJumpMultiplier();

            if (Player.InputHandler.AttackInputs[(int)CombatInputs.PRIMARY])
            {
                StateMachine.ChangeState(Player.PrimaryAttackState);
            } 
            else if (Player.InputHandler.AttackInputs[(int)CombatInputs.SECONDARY])
            {
                StateMachine.ChangeState(Player.SecondaryAttackState);
            }
            else if (_isGrounded && Movement.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(Player.LandState);
            } 
            else if (_isTouchingWall &&!_isTouchingLedge && !_isGrounded)
            {
                StateMachine.ChangeState(Player.LedgeClimbState);
            }
            //else if (_jumpInput && (_isTouchingWall || _isTouchingWallBackwards || _wallJumpCoyoteTime))
            // else if (_jumpInput && _wallJumpCoyoteTime)
            // {
            //     StopWallJumpCoyoteTime();
            //     _isTouchingWall = Player.Core.CollisionSenses.Wall;
            //     Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            //     StateMachine.ChangeState(Player.WallJumpState);
            // }
            else if (_jumpInput && Player.JumpState.CanJump())
            {
                StateMachine.ChangeState(Player.JumpState);
            } 
            else if (_isTouchingWall && _grabInput && !_isGrounded)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
            else if (_isTouchingWall && _xInput == Movement.FacingDirection && Movement.CurrentVelocity.y <= 0f)
            {
                StateMachine.ChangeState(Player.WallSlideState);
            }
            else if (_dashInput && Player.DashState.CheckIfCanDash() && !CollisionSenses.Wall)
            {
                StateMachine.ChangeState(Player.DashState);
            }
            else
            {
                Movement.CheckIfShouldFlip(_xInput);
                Movement.SetVelocityX(PlayerData.movementVelocity * _xInput);
            
                Player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
                Player.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            if (CollisionSenses)
            {
                _oldIsTouchingWall = _isTouchingWall;
                _oldIsTouchingWallBackwards = _isTouchingWallBackwards;
            
                _isGrounded = CollisionSenses.Ground;
                _isTouchingWall = CollisionSenses.Wall;
                _isTouchingWallBackwards = CollisionSenses.WallBackwards;
                _isTouchingLedge = CollisionSenses.LedgeHorizontal;
    
                if (_isTouchingWall && !_isTouchingLedge)
                {
                    Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
                }
    
                if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBackwards && (_oldIsTouchingWall || _oldIsTouchingWallBackwards))
                {
                    StartWallJumpCoyoteTime();
                }
            }
        
        }

        public void StartCoyoteTime() => _coyoteTime = true;
    
        public void StartWallJumpCoyoteTime()
        {
            _startWallJumpCoyoteTime = Time.time;
            _wallJumpCoyoteTime = true;
        }

        public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;
    
        public void SetIsJumping() => _isJumping = true;
    
        private void CheckCoyoteTime()
        {
            if (_coyoteTime && Time.time >= StartTime + PlayerData.coyoteTime)
            {
                _coyoteTime = false;
                Player.JumpState.DecreaseJumps();
            }
        }

        private void CheckWallJumpCoyoteTime()
        {
            if (_wallJumpCoyoteTime && Time.time >= _startWallJumpCoyoteTime + PlayerData.coyoteTime)
            {
                _wallJumpCoyoteTime = false;
            }
        }
    
        private void CheckJumpMultiplier()
        {
            if (_isJumping)
            {
                if (_jumpInputStop)
                {
                    Movement.SetVelocityY(Movement.CurrentVelocity.y * PlayerData.jumpHeightMultiplier);
                    _isJumping = false;
                }
                else if (Movement.CurrentVelocity.y <= 0f)
                {
                    _isJumping = false;
                }
            }
        }
    
    }
}
