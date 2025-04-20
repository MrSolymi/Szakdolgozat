using System;
using Solymi._Scripts.GameManager;
using Solymi.Core.Stats;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.Archer
{
    public class Archer : Entity
    {
        public ArcherIdleState IdleState { get; private set; }
        public ArcherMoveState MoveState { get; private set; }
        public ArcherStunState StunState { get; private set; }
        public ArcherMeleeAttackState MeleeAttackState { get; private set; }
        public ArcherRangedAttackState RangedAttackState { get; private set; }
        public ArcherPlayerDetectedState PlayerDetectedState { get; private set; }
        public ArcherDodgeState DodgeState { get; private set; }
        public ArcherLookForPlayerState LookForPlayerState { get; private set; }
        
        [SerializeField] private Transform meleeAttackPosition, rangedAttackPosition;

        public override void Awake()
        {
            base.Awake();
            
            IdleState = new ArcherIdleState(this, entityData, "idle", this);
            MoveState = new ArcherMoveState(this, entityData, "move", this);
            StunState = new ArcherStunState(this, entityData, "stun", this);
            PlayerDetectedState = new ArcherPlayerDetectedState(this, entityData, "playerDetected", this);
            DodgeState = new ArcherDodgeState(this, entityData, "dodge", this);
            LookForPlayerState = new ArcherLookForPlayerState(this, entityData, "lookForPlayer", this);
            
            MeleeAttackState = new ArcherMeleeAttackState(this, entityData, "meleeAttack", meleeAttackPosition, this);
            RangedAttackState = new ArcherRangedAttackState(this, entityData, "rangedAttack", rangedAttackPosition, this);
            
            Stats.Poise.OnCurrentValueZero += HandlePoiseZero;
            Stats.Health.OnCurrentValueZero += HandleHealthOnZero;
        }
        
        public override void ResetAfterSave()
        {
            base.ResetAfterSave();
            
            StateMachine.ChangeState(IdleState);
        }

        public void Start()
        {
            StateMachine.Initialize(IdleState);
        }


        public override void Update()
        {
            base.Update();
            
            Animator.SetFloat("yVelocity", Movement.RB.velocity.y);
        }

        private void HandleHealthOnZero()
        {
            EntitySaveTracker.RegisterDeath(uniqueId.Id);
        }


        private void HandlePoiseZero() => StateMachine.ChangeState(StunState);
        
        
        private void OnDestroy()
        {
            Stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
            Stats.Health.OnCurrentValueZero -= HandleHealthOnZero;
        }
        
        private void TriggerAttack()
        {
            MeleeAttackState.TriggerAttack();
            RangedAttackState.TriggerAttack();
        }
        
        private void FinishAttack()
        {
            MeleeAttackState.FinishAttack();
            RangedAttackState.FinishAttack();
        }
        
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, entityData.meleeAttackRadius);
        }
    }
}