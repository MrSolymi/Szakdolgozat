using System;
using Solymi.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Solymi.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int numberOfAttacks;
        [SerializeField] private float attackCounterResetCooldown;

        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            private set => _currentAttackCounter = value >= numberOfAttacks ? 0 : value; 
        }
        public event Action OnEnter, OnExit;
        
        private Animator _animator;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }
        

        private AnimationEventHandler _eventHandler;
        
        private int _currentAttackCounter;
        
        private Timer _attackCounterResetTimer;
        public void Enter()
        {
            Debug.Log($"{transform.name} entered");
            
            _attackCounterResetTimer.StopTimer();
            
            _animator.SetBool("active", true);
            _animator.SetInteger("counter", CurrentAttackCounter);
            
            OnEnter?.Invoke();
        }

        private void Exit()
        {
            _animator.SetBool("active", false);
            CurrentAttackCounter++;
            
            _attackCounterResetTimer.StartTimer();
            
            OnExit?.Invoke();
        }

        private void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            
            _animator = BaseGameObject.GetComponent<Animator>();
            
            _eventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
            
            _attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            _attackCounterResetTimer.Tick();
        }

        private void OnEnable()
        {
            _eventHandler.OnFinished += Exit;
            _attackCounterResetTimer.OnTimerEnd += ResetAttackCounter;
        }

        private void OnDisable()
        {
            _eventHandler.OnFinished -= Exit;
            _attackCounterResetTimer.OnTimerEnd -= ResetAttackCounter;
        }
        
        private void ResetAttackCounter() => CurrentAttackCounter = 0;
    }
}