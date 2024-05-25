using Solymi.Player.Data;
using UnityEngine;

namespace Solymi.Player.PlayerStateMachine
{
    public class PlayerState
    {
        protected Core.Core Core;
    
        protected Player Player;
        protected PlayerStateMachine StateMachine;
        protected PlayerData PlayerData;
    
        protected float StartTime;
        protected bool IsAnimationFinished, IsExitingState;
    
        private string _animBoolName;
    
    
        public PlayerState(Player player, PlayerData playerData, string animBoolName)
        {
            Player = player;
            StateMachine = player.StateMachine;
            PlayerData = playerData;
            _animBoolName = animBoolName;
            Core = player.Core;
        }
    
        public virtual void Enter()
        {
            DoChecks();
            Player.Animator.SetBool(_animBoolName, true);
            StartTime = Time.time;
            IsAnimationFinished = false;
            IsExitingState = false;
            //Debug.Log("Entered idle state");
        }
    
        public virtual void Exit()
        {
            Player.Animator.SetBool(_animBoolName, false);
            IsExitingState = true;
        }
    
        public virtual void LogicUpdate() { }
    
        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }
    
        public virtual void DoChecks() { }
    
        public virtual void AnimationTrigger() { }
    
        public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;

    }
}
