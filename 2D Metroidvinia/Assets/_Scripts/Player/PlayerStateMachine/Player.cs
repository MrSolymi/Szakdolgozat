using System;
using Solymi._Scripts.GameManager;
using Solymi._Scripts.Scene;
using Solymi.Core.CoreComponents;
using Solymi.Player.Data;
using Solymi.Player.Input;
using Solymi.Player.PlayerStates.SubStates;
using Solymi.Weapons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Solymi.Player.PlayerStateMachine
{
    public class Player : MonoBehaviour
    {
        public Core.Core Core { get; private set; }
        
        protected Stats Stats;
    
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallGrabState WallGrabState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerLedgeClimbState LedgeClimbState { get; private set; }
        public PlayerAttackState PrimaryAttackState { get; private set; }
        public PlayerAttackState SecondaryAttackState { get; private set; }
    
        public PlayerDashState DashState { get; private set; }
        public Animator Animator { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
    
        //public Rigidbody2D RB { get; private set; }
    
        [SerializeField] private PlayerData playerData;
    
        private Weapon _primaryWeapon, _secondaryWeapon;
        
        private PlayerInput _playerInput;
    
        private void Awake()
        {
            Core = GetComponentInChildren<Core.Core>();
            
            Stats = Core.GetCoreComponent<Stats>();
        
            _primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
            _secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
            
            _playerInput = GetComponent<PlayerInput>();
            
            _primaryWeapon.SetCore(Core);
            _secondaryWeapon.SetCore(Core);
        
            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(this, playerData, "idle");
            MoveState = new PlayerMoveState(this, playerData, "move");
            JumpState = new PlayerJumpState(this, playerData, "inAir");
            InAirState = new PlayerInAirState(this, playerData, "inAir");
            LandState = new PlayerLandState(this, playerData, "land");
            WallSlideState = new PlayerWallSlideState(this, playerData, "wallSlide");
            WallGrabState = new PlayerWallGrabState(this, playerData, "wallGrab");
            WallJumpState = new PlayerWallJumpState(this, playerData, "inAir");
            LedgeClimbState = new PlayerLedgeClimbState(this, playerData, "ledgeClimbState");
            DashState = new PlayerDashState(this, playerData, "inAir");
            PrimaryAttackState = new PlayerAttackState(this, playerData, "attack", _primaryWeapon);
            SecondaryAttackState = new PlayerAttackState(this, playerData, "attack", _secondaryWeapon);

            if (!GameManager.HasActivatedCampfire())
            {
                Stats.RespawnPoint.Initialize();
                GameManager.Instance.savedGamePosition.Set(Stats.RespawnPoint.DefaultRespawnPoint.x, Stats.RespawnPoint.DefaultRespawnPoint.y);
                GameManager.Instance.currentGameSceneName = "GameStarterScene";
            }
            else
            {
                Stats.RespawnPoint.SetRespawnPointSceneName(GameManager.Instance.savedGameSceneName);
                Stats.RespawnPoint.SetRespawnPoint(GameManager.Instance.savedGamePosition);
            }
            
            
        }

        private void Start()
        {
            Animator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            //RB = GetComponent<Rigidbody2D>();
        
            //FacingDirection = 1;
        
            StateMachine.Initialize(IdleState);
        }
    
        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
            
            //Debug.LogWarning(Stats.Health.CurrentValue);
        }
    
        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnDisable()
        {
            //Debug.LogError("Player is currently disabled");
            if (GameManager.Instance.playerDeathUI != null)
            {
                _playerInput.SwitchCurrentActionMap("UI");
                GameManager.Instance.playerDeathUI.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void OnEnable()
        {
            _playerInput.SwitchCurrentActionMap("Gameplay");
        }


        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

        public void Respawn()
        {
            GameManager.Instance.playerDeathUI.SetActive(false);
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            var rnd = Mathf.Round(Random.Range(-1.2f, 1.2f) * 100f) / 100f;
            var resPoint = new Vector2();
            if (Stats.RespawnPoint.CurrentRespawnPointSceneName.Equals(Stats.RespawnPoint.DefaultRespawnPointSceneName) && 
                Stats.RespawnPoint.CurrentRespawnPoint.Equals(Stats.RespawnPoint.DefaultRespawnPoint))
            {
                resPoint.Set(
                    Stats.RespawnPoint.DefaultRespawnPoint.x + rnd,
                    Stats.RespawnPoint.DefaultRespawnPoint.y + 0.5f);
                
                FindObjectOfType<FadeController>().FadeToBlackAndLoadScene(
                    Stats.RespawnPoint.DefaultRespawnPointSceneName, resPoint);
            }
            else
            {
                resPoint.Set(
                    Stats.RespawnPoint.CurrentRespawnPoint.x + rnd,
                    Stats.RespawnPoint.CurrentRespawnPoint.y + 0.5f);
            
                FindObjectOfType<FadeController>().FadeToBlackAndLoadScene(
                    Stats.RespawnPoint.CurrentRespawnPointSceneName, resPoint);
            }
            
            Stats.Health.Reset();
            StateMachine.Initialize(IdleState);
            
            _primaryWeapon.EventHandler.ForceFinish();
            _secondaryWeapon.EventHandler.ForceFinish();
        }
    }
}
