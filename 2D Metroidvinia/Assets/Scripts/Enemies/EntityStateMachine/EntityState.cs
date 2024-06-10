using Solymi.Enemies.Data;
using UnityEngine;

namespace Solymi.Enemies.EntityStateMachine
{
    public class EntityState
    {
        protected Core.Core Core;
    
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
            DoChecks();
            
            StartTime = Time.time;
            Entity.Animator.SetBool(_animBoolName, true);
            
            IsAnimationFinished = false;
            IsExitingState = false;
        }

        public virtual void Exit()
        {
            Entity.Animator.SetBool(_animBoolName, false);
            IsExitingState = true;
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
        public virtual void AnimationTrigger() { }
    
        public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;
    }
}
