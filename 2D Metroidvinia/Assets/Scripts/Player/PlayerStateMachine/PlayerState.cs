using UnityEngine;

public class PlayerState
{
    protected Core _core;
    
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected PlayerData _playerData;
    
    protected float _startTime;
    
    private string _animBoolName;
    
    public PlayerState(Player player, PlayerData playerData, string animBoolName)
    {
        _player = player;
        _stateMachine = player.StateMachine;
        _playerData = playerData;
        _animBoolName = animBoolName;
        _core = player.Core;
    }
    
    public virtual void Enter()
    {
        DoChecks();
        _player.Animator.SetBool(_animBoolName, true);
        _startTime = Time.time;
        //Debug.Log("Entered idle state");
    }
    
    public virtual void Exit()
    {
        _player.Animator.SetBool(_animBoolName, false);
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
}
