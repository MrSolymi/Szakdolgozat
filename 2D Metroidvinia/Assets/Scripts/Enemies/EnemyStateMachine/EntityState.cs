using UnityEngine;

public class EntityState
{
    protected Core Core;
    
    protected Entity Entity;
    protected EntityStateMachine StateMachine;
    protected EntityData EntityData;
    
    protected float StartTime;
    protected bool IsAnimationFinished, IsExitingState;
    
    private string _animBoolName;

    public EntityState(Entity entity, EntityData entityData, string animBoolName)
    {
        Entity = entity;
        StateMachine = entity.StateMachine;
        _animBoolName = animBoolName;
        Core = entity.Core;
        EntityData = entityData;
    }

    public virtual void Enter()
    {
        StartTime = Time.time;
        Entity.Animator.SetBool(_animBoolName, true);
        
        DoChecks();
    }

    public virtual void Exit()
    {
        Entity.Animator.SetBool(_animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        
    }
    
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    
    public virtual void DoChecks()
    {
        
    }
}
