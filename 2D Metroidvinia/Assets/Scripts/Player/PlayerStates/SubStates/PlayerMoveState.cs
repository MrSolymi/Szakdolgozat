using Solymi.Player.Data;
using Solymi.Player.PlayerStates.SuperStates;

namespace Solymi.Player.PlayerStates.SubStates
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
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
        
            Movement.CheckIfShouldFlip(XInput);
        
            Movement.SetVelocityX(PlayerData.movementVelocity * XInput);
        
            if (XInput == 0 && !IsExitingState)
            {
                StateMachine.ChangeState(Player.IdleState);
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
    }
}
