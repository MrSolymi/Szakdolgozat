using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Core Core { get; private set; }
    
    public EntityStateMachine StateMachine { get; private set; }
    
    [SerializeField] protected EntityData entityData;
    public Animator Animator { get; private set; }
    
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        StateMachine = new EntityStateMachine();
    }

    public virtual void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    // public virtual void Testing()
    // {
    //     Debug.Log(Core.Movement.RB==null);
    // }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        //Debug.Log(EntityData.whatIsGround);
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    
    // public void OnDrawGizmos()
    // {
    //     Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * EntityData.wallCheckDistance));
    //     Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * EntityData.ledgeCheckDistance));
    // }
}
