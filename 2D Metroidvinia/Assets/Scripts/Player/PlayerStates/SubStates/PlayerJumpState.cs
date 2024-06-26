using Solymi.Player.Data;
using Solymi.Player.PlayerStates.SuperStates;

namespace Solymi.Player.PlayerStates.SubStates
{
    public class PlayerJumpState : PlayerAbilityState
    {
        private int _amountOfJumpsLeft;
        public PlayerJumpState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
        {
            _amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
        
            Player.InputHandler.UseJumpInput();
            Movement.SetVelocityY(PlayerData.jumpVelocity);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            Player.InAirState.SetIsJumping();
        }
    
        public bool CanJump() => _amountOfJumpsLeft > 0;
    
        public void ResetJumps() => _amountOfJumpsLeft = PlayerData.amountOfJumps;
    
        public void DecreaseJumps() => _amountOfJumpsLeft--;
    }
}
