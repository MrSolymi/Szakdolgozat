using Solymi.Core.CoreComponents;
using Solymi.Enums;
using Solymi.Player.Data;
using Solymi.Player.PlayerStateMachine;

namespace Solymi.Player.PlayerStates.SuperStates
{
    public class PlayerGroundedState : PlayerState
    {
        protected int XInput;
    
        private bool _jumpInput, _isGrounded, _isTouchingWall, _grabInput, _dashInput;

        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        protected CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
    
        public PlayerGroundedState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
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

            if (Player.InputHandler.AttackInputs[(int)CombatInputs.PRIMARY])
            {
                Player.InputHandler.UseAttackInput((int)CombatInputs.PRIMARY);
                StateMachine.ChangeState(Player.PrimaryAttackState);
            } 
            else if (Player.InputHandler.AttackInputs[(int)CombatInputs.SECONDARY])
            {
                Player.InputHandler.UseAttackInput((int)CombatInputs.SECONDARY);
                StateMachine.ChangeState(Player.SecondaryAttackState);
            }
            else if (_jumpInput && Player.JumpState.CanJump())
            {
                StateMachine.ChangeState(Player.JumpState);
            } 
            else if (!_isGrounded)
            {
                Player.InAirState.StartCoyoteTime();
                StateMachine.ChangeState(Player.InAirState);
            } 
            else if (_isTouchingWall && _grabInput && !_isGrounded)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
            else if (_dashInput && Player.DashState.CheckIfCanDash() && (CollisionSenses.Ground && !CollisionSenses.Wall || !CollisionSenses.Ground && !CollisionSenses.Wall || !CollisionSenses.Ground && CollisionSenses.Wall))
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
        
            if (CollisionSenses)
            {
                _isGrounded = CollisionSenses.Ground;
                _isTouchingWall = CollisionSenses.Wall;
            }
        }
    }
}
