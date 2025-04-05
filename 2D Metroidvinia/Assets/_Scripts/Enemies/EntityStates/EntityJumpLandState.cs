using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityJumpLandState : EntityState
    {
        protected bool isPlayerInMinAgroRange, isPlayerInMaxAgroRange;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        public EntityJumpLandState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            isPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
            isPlayerInMaxAgroRange = CollisionSenses.PlayerInMaxAgroRange;
        }
    }
}