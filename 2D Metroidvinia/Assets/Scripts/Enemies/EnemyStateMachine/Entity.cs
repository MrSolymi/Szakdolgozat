using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //public Core Core { get; private set; }
    
    public EntityStateMachine StateMachine { get; private set; }
    
    [SerializeField] protected EntityData entityData;
    public Rigidbody2D RB { get; private set; }
    public Animator Animator { get; private set; }

    public int FacingDirection { get; private set; }
    private Vector2 _workspace;
    
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    
    // public virtual void Awake()
    // {
    //     //Core = GetComponentInChildren<Core>();
    //     
    //     FacingDirection = 1;
    //     StateMachine = new EntityStateMachine();
    // }

    public virtual void Start()
    {
        FacingDirection = 1;
        StateMachine = new EntityStateMachine();
        
        Animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        //Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    // public virtual void Testing()
    // {
    //     Debug.Log(entityData==null);
    // }
    public virtual void SetVelocity(float velocity)
    {
        _workspace.Set(velocity * FacingDirection, RB.velocity.y);
        RB.velocity = _workspace;
    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, RB.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        //Debug.Log(EntityData.whatIsGround);
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    
    // public void OnDrawGizmos()
    // {
    //     Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * EntityData.wallCheckDistance));
    //     Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * EntityData.ledgeCheckDistance));
    // }
}
