using System;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
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
        
        //_player.CheckIfShouldFlip(xInput);
        Core.Movement.CheckIfShouldFlip(XInput);
        
        //_player.SetVelocityX(_playerData.movementVelocity * xInput);
        Core.Movement.SetVelocityX(PlayerData.movementVelocity * XInput);
        
        if (XInput == 0)
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