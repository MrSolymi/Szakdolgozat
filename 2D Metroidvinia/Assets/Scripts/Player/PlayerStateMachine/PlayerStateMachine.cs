using UnityEngine;

namespace Solymi.Player.PlayerStateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }
    
        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            //Debug.Log(newState.GetType());
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
