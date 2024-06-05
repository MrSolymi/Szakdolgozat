using System;
using Solymi.Utilities;
using Solymi.Weapons.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Solymi.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public Core.Core Core { get; private set; }
        
        [SerializeField] private float attackCounterResetCooldown;
        
        public WeaponData WeaponData {get; private set;}

        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            private set => _currentAttackCounter = value >= WeaponData.NumberOfAttacks ? 0 : value; 
        }
        
        public void SetCore(Core.Core core) => Core = core;
        public void SetWeaponData(WeaponData weaponData) => WeaponData = weaponData;
        public event Action OnEnter, OnExit;
        
        private Animator _animator;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }
        

        public AnimationEventHandler EventHandler { get; private set; }
        
        private int _currentAttackCounter;
        
        private Timer _attackCounterResetTimer;
        public void Enter()
        {
            //Debug.Log($"{transform.name} entered");
            
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
            
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
            
            _attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            _attackCounterResetTimer.Tick();
        }

        private void OnEnable()
        {
            EventHandler.OnFinished += Exit;
            _attackCounterResetTimer.OnTimerEnd += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinished -= Exit;
            _attackCounterResetTimer.OnTimerEnd -= ResetAttackCounter;
        }
        
        private void ResetAttackCounter() => CurrentAttackCounter = 0;
    }
}