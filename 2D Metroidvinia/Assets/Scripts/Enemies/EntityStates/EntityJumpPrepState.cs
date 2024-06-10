using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityJumpPrepState : EntityState
    {
        protected bool isGrounded;
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        public EntityJumpPrepState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            isGrounded = CollisionSenses.Ground;
        }
    }
}