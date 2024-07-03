using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityDeathState : EntityState
    {
        protected Death Death => _death ? _death : Core.GetCoreComponent(ref _death);
        private Death _death;
        
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        public EntityDeathState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Movement.SetVelocityZero();
        }
    }
}