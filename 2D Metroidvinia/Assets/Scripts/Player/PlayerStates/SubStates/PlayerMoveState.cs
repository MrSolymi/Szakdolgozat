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
        _core.Movement.CheckIfShouldFlip(xInput);
        
        //_player.SetVelocityX(_playerData.movementVelocity * xInput);
        _core.Movement.SetVelocityX(_playerData.movementVelocity * xInput);
        
        if (xInput == 0)
        {
            _stateMachine.ChangeState(_player.IdleState);
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
