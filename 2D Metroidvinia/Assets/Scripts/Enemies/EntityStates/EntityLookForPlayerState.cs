using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.EntityStates
{
    public class EntityLookForPlayerState : EntityState
    {
        protected bool flipAfterEnter, isPlayerInMinAgroRange, isAllTurnsDone, isTurningTimesOver;
        protected float lastTurnTime;
        protected int amountOfTurnsDone;
        
        protected CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        public EntityLookForPlayerState(Entity entity, EntityData entityData, string animBoolName) : base(entity, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            isAllTurnsDone = false;
            isTurningTimesOver = false;
            
            lastTurnTime = StartTime;
            amountOfTurnsDone = 0;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (flipAfterEnter)
            {
                Movement.Flip();
                lastTurnTime = Time.time;
                amountOfTurnsDone++;
                flipAfterEnter = false;
            }
            else if (Time.time >= lastTurnTime + EntityData.timeBetweenTurns && !isAllTurnsDone)
            {
                Movement.Flip();
                lastTurnTime = Time.time;
                amountOfTurnsDone++;
            }

            if (amountOfTurnsDone >= EntityData.amountOfTurns)
            {
                isAllTurnsDone = true;
            }

            if (Time.time >= lastTurnTime + EntityData.timeBetweenTurns && isAllTurnsDone)
            {
                isTurningTimesOver = true;
            }
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            isPlayerInMinAgroRange = CollisionSenses.PlayerInMinAgroRange;
        }
        
        public void SetFlipAfterEnter(bool flip)
        {
            flipAfterEnter = flip;
        }
    }
}