using System;
using Solymi.Core.CoreComponents;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Interfaces;
using Solymi.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Solymi.Enemies.Slime
{
    public class Slime : Entity
    {
        [SerializeField] private GameObject littleSlimePrefab;
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
            
            Stats.Health.OnCurrentValueZero += HandleHealthZero;
        }
        
        private Timer _attackCooldownTimer;
        private bool _canAttack = true;
        public void Start()
        {
            StateMachine.Initialize(IdleState);
            
            // Inicializáljuk a cooldown timer-t 1 másodperces időtartammal
            _attackCooldownTimer = new Timer(entityData.collisionDamageCooldown); // 1 másodperces cooldown
            _attackCooldownTimer.OnTimerEnd += ResetAttackCooldown; // Ha lejár a cooldown, engedjük a támadást
        }

        public override void Update()
        {
            base.Update();
            
            _attackCooldownTimer.Tick();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void HandleHealthZero()
        {
            var littleSlimesContainer = GameObject.Find("LittleSlimes");
            
            var rnd = Random.Range(4, 6);
            for (var i = 0; i < rnd; i++)
            {
                var offsetX = Random.Range(-2f, 2f);
                var offsetY = Random.Range(0f, 1f);
                
                var spawnPosition = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
                
                Instantiate(littleSlimePrefab, spawnPosition, Quaternion.Euler(0f, 0f, 0f), littleSlimesContainer.transform);
                
            }
        }

        private void OnDestroy()
        {
            Stats.Health.OnCurrentValueZero -= HandleHealthZero;
        
        }

        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }
        
        private void ResetAttackCooldown()
        {
            _canAttack = true; // A cooldown lejárt, most már támadhatunk
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            try
            {
                
                if (other.gameObject.transform.parent.parent.CompareTag("Player") && _canAttack)
                {
                    Debug.LogWarning(gameObject.name + " : " + other.gameObject.name);
                    
                    if (other.GetComponent<DamageReceiver>().TryGetComponent(out IDamageable damageable))
                    {
                        //Debug.LogError(damageable);
                        damageable.Damage(entityData.collisionDamage);
                    }

                    if (other.GetComponent<KnockBackReceiver>().TryGetComponent(out IKnockBackable knockBackable))
                    {
                        //Debug.LogError(knockBackable);
                        knockBackable.KnockBack(entityData.knockBackAngle, entityData.knockBackStrength, Movement.FacingDirection);
                    }
                    
                    // Indítjuk el a cooldown-ot
                    _attackCooldownTimer.StartTimer();
                    _canAttack = false; // Aktiváljuk a cooldown-t, nem támadhatunk újra azonnal
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}