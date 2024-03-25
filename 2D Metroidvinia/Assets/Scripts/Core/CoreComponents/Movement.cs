using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    
    private Vector2 _workspace;

    protected override void Awake()
    {
        base.Awake();
        
        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }
    
    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }
    
    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    
    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            //Debug.Log("CheckIfShouldFlip");
            Flip();
        }
    }
}
