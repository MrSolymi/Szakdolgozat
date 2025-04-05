using Solymi.Core.CoreComponents;
using Solymi.Player.Data;
using Solymi.Player.PlayerStateMachine;

namespace Solymi.Player.PlayerStates.SuperStates
{
    public class PlayerAbilityState : PlayerState
    {
        protected bool IsAbilityDone;
    
        private bool _isGrounded;
    
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        protected CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        public PlayerAbilityState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        
            IsAbilityDone = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsAbilityDone)
            {
                if (_isGrounded && Movement.CurrentVelocity.y < 0.01f)
                {
                    StateMachine.ChangeState(Player.IdleState);
                }
                else
                {
                    StateMachine.ChangeState(Player.InAirState);
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

            if (CollisionSenses)
            {
                _isGrounded = CollisionSenses.Ground;
            }
        
        }
    }
}
