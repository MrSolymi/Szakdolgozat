using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityMoveState : EntityState
    {
        protected bool isDetectingWall,  isDetectingLedge, isPlayerInMinAgroRange, isDamaged;
    
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected Stats Stats => _stats ? _stats : Core.GetCoreComponent(ref _stats);
        private Stats _stats;
        public EntityMoveState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }
        
        public override void DoChecks()
        {
            base.DoChecks();
        
            //Entity.Testing();
        
            isDetectingLedge = CollisionSenses.LedgeVertical;
            isDetectingWall = CollisionSenses.Wall;
            isPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
            isDamaged = Stats.IsDamaged;
        }
    }
}
