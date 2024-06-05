using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityChargeState : EntityState
    {
        protected bool isPlayerInMinAgroRange, isDetectingWall, isDetectingLedge, isChargeTimeOver, doCloseRangeAction;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        public EntityChargeState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            isChargeTimeOver = false;
        }

        public override void DoChecks()
        {
            base.DoChecks();

            isPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
            isDetectingWall = CollisionSenses.Wall;
            isDetectingLedge = CollisionSenses.LedgeVertical;
            
            doCloseRangeAction = CollisionSenses.PlayerInCloseRangeAction;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + EntityData.chargeTime && !isChargeTimeOver)
            {
                isChargeTimeOver = true;
            }
        }
    }
}