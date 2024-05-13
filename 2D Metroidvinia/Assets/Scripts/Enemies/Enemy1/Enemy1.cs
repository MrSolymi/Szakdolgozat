using UnityEngine;

public class Enemy1 : Entity
{
    public Enemy1IdleState idleState { get; private set; }
    public Enemy1MoveState moveState { get; private set; }
    
    
    public override void Awake()
    {
        base.Awake();
        
        moveState = new Enemy1MoveState(this, entityData, "move", this);
        idleState = new Enemy1IdleState(this, entityData, "idle", this);
    }

    public  void Start()
    {
        StateMachine.Initialize(idleState);
    }
    
}
