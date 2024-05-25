using Solymi.Player.Data;
using Solymi.Player.PlayerStates.SuperStates;
using Solymi.Weapons;

namespace Solymi.Player.PlayerStates.SubStates
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private Weapon _weapon;
    
        public PlayerAttackState(PlayerStateMachine.Player player, PlayerData playerData, string animBoolName, Weapon weapon) : base(player, playerData, animBoolName)
        {
            _weapon = weapon;
        
            _weapon.OnExit += ExitHandler;
        }

        public override void Enter()
        {
            base.Enter();
            //IsAbilityDone = false;
        
        
            _weapon.Enter();
        }

        private void ExitHandler()
        {
            AnimationFinishTrigger();
            IsAbilityDone = true;
        }
    }
}
