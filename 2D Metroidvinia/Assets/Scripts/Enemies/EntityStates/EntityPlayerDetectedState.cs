using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityPlayerDetectedState : EntityState
    {
        protected bool isPlayerInMinAgroRange, isPlayerInMaxAgroRange, doLongRangeAction, doCloseRangeAction, isDetectingLedge;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        public EntityPlayerDetectedState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            doLongRangeAction = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + EntityData.timeBeforeLongRangeAction)
            {
                doLongRangeAction = true;
            }
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            isPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
            isPlayerInMaxAgroRange = CollisionSenses.PlayerInMaxAgroRange;
            
            isDetectingLedge = CollisionSenses.LedgeVertical;
            
            doCloseRangeAction = CollisionSenses.PlayerInCloseRangeAction;
        }
    }
}