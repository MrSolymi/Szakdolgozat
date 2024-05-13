using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Core Core { get; private set; }
    
    public EntityStateMachine StateMachine { get; private set; }
    
    [SerializeField] protected EntityData entityData;
    public Animator Animator { get; private set; }

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        Animator = GetComponent<Animator>();
        
        StateMachine = new EntityStateMachine();
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
    
    public virtual void OnDrawGizmos()
    {
        if (!Core) return;
        Gizmos.DrawLine(Core.CollisionSenses.WallCheck.position, Core.CollisionSenses.WallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * Core.CollisionSenses.WallCheckDistance));
        Gizmos.DrawLine(Core.CollisionSenses.LedgeCheckVertical.position, Core.CollisionSenses.LedgeCheckVertical.position + (Vector3)(Vector2.down * Core.CollisionSenses.WallCheckDistance));

    }
}
