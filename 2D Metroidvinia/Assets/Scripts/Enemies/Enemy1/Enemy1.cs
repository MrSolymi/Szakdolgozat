using UnityEngine;

public class Enemy1 : Entity
{
    public Enemy1IdleState idleState { get; private set; }
    public Enemy1MoveState moveState { get; private set; }
    
    
    public override void Start()
    {
        base.Start();
        
        moveState = new Enemy1MoveState(this, entityData, "move", this);
        idleState = new Enemy1IdleState(this, entityData, "idle", this);
        
        StateMachine.Initialize(moveState);
    }
}
