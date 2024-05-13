using UnityEngine;

public class EntityMoveState : EntityState
{
    protected bool isDetectingWall,  isDetectingLedge;
    
    public EntityMoveState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Core.Movement.SetVelocityX(EntityData.movementSpeed * Core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        //Core.Movement.SetVelocityX(EntityData.movementSpeed * Core.Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        //Entity.Testing();
        
        isDetectingLedge = Entity.CheckLedge();
        isDetectingWall = Entity.CheckWall();
    }
}
