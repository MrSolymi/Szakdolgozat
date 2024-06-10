using System;
using Solymi.Enemies.EntityStateMachine;
using UnityEngine;

namespace Solymi.Enemies.Enemy1
{
    public class Enemy1 : Entity
    {
        public Enemy1IdleState IdleState { get; private set; }
        public Enemy1MoveState MoveState { get; private set; }
        public Enemy1PlayerDetectedState PlayerDetectedState { get; private set; }
        public Enemy1ChargeState ChargeState { get; private set; }
        public Enemy1LookForPlayerState LookForPlayerState { get; private set; }
        public Enemy1MeleeAttackState MeleeAttackState { get; private set; }
        public Enemy1StunState StunState { get; private set; }
        
        [SerializeField] private Transform meleeAttackPosition;
        
        public override void Awake()
        {
            base.Awake();
        
            MoveState = new Enemy1MoveState(this, entityData, "move", this);
            IdleState = new Enemy1IdleState(this, entityData, "idle", this);
            PlayerDetectedState = new Enemy1PlayerDetectedState(this, entityData, "playerDetected", this);
            ChargeState = new Enemy1ChargeState(this, entityData, "charge", this);
            LookForPlayerState = new Enemy1LookForPlayerState(this, entityData, "lookForPlayer", this);
            MeleeAttackState = new Enemy1MeleeAttackState(this, entityData, "meleeAttack", meleeAttackPosition, this);
            StunState = new Enemy1StunState(this, entityData, "stun", this);
            
            Stats.Poise.OnCurrentValueZero += HandlePoiseZero;
            //Stats.Health.OnCurrentValueZero += HandleHealthZero;
        }

        public  void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, entityData.meleeAttackRadius);
        }

        private void HandlePoiseZero()
        {
            StateMachine.ChangeState(StunState);
        }
        
        // private void HandleHealthZero()
        // {
        //     //gameObject.SetActive(false);
        // }

        private void OnDestroy()
        {
            Stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
            //Stats.Health.OnCurrentValueZero -= HandleHealthZero;
        }

        private void TriggerAttack()
        {
            MeleeAttackState.TriggerAttack();
        }
        private void FinishAttack()
        {
            MeleeAttackState.FinishAttack();
        }
    }
}
