using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    public PlayerAttackState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        
        _weapon.ExitWeapon();
    }
    
    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        
        weapon.InitializeWeapon(this);
    }
    
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        IsAbilityDone = true;
    }
}
