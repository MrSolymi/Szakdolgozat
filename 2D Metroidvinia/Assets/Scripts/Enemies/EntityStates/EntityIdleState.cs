using UnityEngine;

public class EntityIdleState : EntityState
{
    protected bool flipAfterIdle, isIdleTimeOver;
    
    protected float idleTime;
    
    protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
    private Movement _movement;
    public EntityIdleState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Movement.SetVelocityX(0.0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            Movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + idleTime)
        {
            isIdleTimeOver = true;
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
    
    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }
    
    public void SetRandomIdleTime()
    {
        idleTime = Random.Range(EntityData.minIdleTime, EntityData.maxIdleTime);
    }
}
