using UnityEngine;

public class Enemy1IdleState : EntityIdleState
{
    private Enemy1 _enemy;
    
    public Enemy1IdleState(Entity entity, EntityData entityData, string animBoolName, Enemy1 enemy) : base(entity, entityData, animBoolName)
    {
        _enemy = enemy;
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

        if (isIdleTimeOver)
        {
            StateMachine.ChangeState(_enemy.moveState);
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
