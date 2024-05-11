using UnityEngine;

public class Player : MonoBehaviour
{
    public Core Core { get; private set; }
    
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
    
    public PlayerDashState DashState { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    
    [SerializeField] private PlayerData playerData;
    
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
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
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        
        //FacingDirection = 1;
        
        StateMachine.Initialize(IdleState);
    }
    
    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }
    
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
