using Solymi.Enemies.EntityStateMachine;

namespace Solymi.Enemies.Slime
{
    public class Slime : Entity
    {
        public SlimeIdleState IdleState { get; private set; }
        public SlimePlayerDetectedState PlayerDetectedState { get; private set; }
        public SlimeLookForPlayerState LookForPlayerState { get; private set; }
        public SlimeJumpPrepState JumpPrepState { get; private set; }
        public SlimeJumpState JumpState { get; private set; }
        public SlimeJumpLandState JumpLandState { get; private set; }
        public override void Awake()
        {
            base.Awake();
            
            IdleState = new SlimeIdleState(this, entityData, "idle", this);
            PlayerDetectedState = new SlimePlayerDetectedState(this, entityData, "playerDetected", this);
            LookForPlayerState = new SlimeLookForPlayerState(this, entityData, "lookForPlayer", this);
            JumpPrepState = new SlimeJumpPrepState(this, entityData, "jumpPrep", this);
            JumpState = new SlimeJumpState(this, entityData, "jump", this);
            JumpLandState = new SlimeJumpLandState(this, entityData, "jumpLand", this);
        }
        
        public void Start()
        {
            StateMachine.Initialize(IdleState);
        }
        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    }
}