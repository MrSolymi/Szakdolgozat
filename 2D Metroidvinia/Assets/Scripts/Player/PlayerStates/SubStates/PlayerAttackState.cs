using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    
    private int xInput;
    private float _velocityToSet;
    private bool setVelocity, shouldCheckFlip;
        
    public PlayerAttackState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        IsAbilityDone = false;
        
        setVelocity = false;
        
        _weapon.EnterWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = Player.InputHandler.NormalizedInputX;
        
        if (shouldCheckFlip)
        {
            Core.Movement.CheckIfShouldFlip(xInput);
        }
        
        if (setVelocity)
        {
            Core.Movement.SetVelocityX(_velocityToSet * Core.Movement.FacingDirection);
        }
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
    
    public void SetPlayerVelocity(float velocity)
    {
        Core.Movement.SetVelocityX(velocity * Core.Movement.FacingDirection);
        _velocityToSet = velocity;
        setVelocity = true;
    }
    
    public void SetShouldCheckFlip(bool value)
    {
        shouldCheckFlip = value;
    }
}
