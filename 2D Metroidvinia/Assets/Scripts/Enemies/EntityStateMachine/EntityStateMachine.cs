namespace Solymi.Enemies.EntityStateMachine
{
    public class EntityStateMachine
    {
        public EntityState CurrentState { get; private set; }
    
        public void Initialize(EntityState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        public void ChangeState(EntityState newState)
        {
            CurrentState.Exit();
            //Debug.Log(newState.GetType());
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
