using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityMoveState : EntityState
    {
        protected bool isDetectingWall,  isDetectingLedge;
    
    
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        public EntityMoveState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        
            Movement.SetVelocityX(EntityData.movementSpeed * Movement.FacingDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            Movement.SetVelocityX(EntityData.movementSpeed * Movement.FacingDirection);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        
            //Entity.Testing();
        
            isDetectingLedge = CollisionSenses.LedgeVertical;
            isDetectingWall = CollisionSenses.Wall;
        }
    }
}
