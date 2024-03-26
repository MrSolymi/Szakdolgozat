using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
        _amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        
        Core.Movement.SetVelocityY(PlayerData.jumpVelocity);
        IsAbilityDone = true;
        _amountOfJumpsLeft--;
        Player.InAirState.SetIsJumping();
    }
    
    public bool CanJump() => _amountOfJumpsLeft > 0;
    
    public void ResetJumps() => _amountOfJumpsLeft = PlayerData.amountOfJumps;
    
    public void DecreaseJumps() => _amountOfJumpsLeft--;
}
