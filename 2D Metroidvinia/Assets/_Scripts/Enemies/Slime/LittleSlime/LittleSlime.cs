using Solymi.Core.CoreComponents;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;
using UnityEngine;

namespace Solymi.Enemies.Slime.LittleSlime
{
    public class LittleSlime : Entity
    {
        public LittleSlimeIdleState IdleState { get; private set; }
        public LittleSlimePlayerDetectedState PlayerDetectedState { get; private set; }
        public LittleSlimeLookForPlayerState LookForPlayerState { get; private set; }
        public LittleSlimeJumpPrepState JumpPrepState { get; private set; }
        public LittleSlimeJumpState JumpState { get; private set; }
        public LittleSlimeJumpLandState JumpLandState { get; private set; }
        public override void Awake()
        {
            base.Awake();
            
            IdleState = new LittleSlimeIdleState(this, entityData, "idle", this);
            PlayerDetectedState = new LittleSlimePlayerDetectedState(this, entityData, "playerDetected", this);
            LookForPlayerState = new LittleSlimeLookForPlayerState(this, entityData, "lookForPlayer", this);
            JumpPrepState = new LittleSlimeJumpPrepState(this, entityData, "jumpPrep", this);
            JumpState = new LittleSlimeJumpState(this, entityData, "jump", this);
            JumpLandState = new LittleSlimeJumpLandState(this, entityData, "jumpLand", this);
        }
        
        public void Start()
        {
            StateMachine.Initialize(IdleState);
        }


        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    }
}