using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollisionSenses : CoreComponent
{
    [SerializeField] private Transform groundCheck;
    public Transform GroundCheck
    {
        get
        {
            if (groundCheck) return groundCheck;
            
            Debug.LogError("No groundCheck found on " + _core.transform.parent.name);
            return null;
        }   
        private set => groundCheck = value;
    }

    [SerializeField] private Transform wallCheck;
    public Transform WallCheck
    {
        get
        {
            if (wallCheck) return wallCheck;
            
            Debug.LogError("No wallCheck found on " + _core.transform.parent.name);
            return null;
        }
        private set => wallCheck = value;
    }
    
    [SerializeField] private Transform ledgeCheckHorizontal;
    public Transform LedgeCheckHorizontal
    {
        get
        {
            if (ledgeCheckHorizontal) return ledgeCheckHorizontal;
            
            Debug.LogError("No ledgeCheckHorizontal found on " + _core.transform.parent.name);
            return null;
        } 
        private set => ledgeCheckHorizontal = value;
    }

    [SerializeField] private Transform ledgeCheckVertical;
    public Transform LedgeCheckVertical
    {
        get
        {
            if (ledgeCheckVertical) return ledgeCheckVertical;
            
            Debug.LogError("No ledgeCheckVertical found on " + _core.transform.parent.name);
            return null;
        }
        private set => ledgeCheckVertical = value;
    }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    [SerializeField] private float groundCheckRadius;
    
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    [SerializeField] private float wallCheckDistance;
    
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    [SerializeField] private LayerMask whatIsGround;

    // public bool CheckIfGrounded()
    // {
    //     return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    // }

    public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    
    public bool Wall => Physics2D.Raycast(WallCheck.position, Vector2.right * _core.Movement.FacingDirection ,wallCheckDistance, whatIsGround);
    
    public bool WallBackwards => Physics2D.Raycast(WallCheck.position, Vector2.right * -_core.Movement.FacingDirection ,wallCheckDistance, whatIsGround);
    
    public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * _core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    
    public bool LedgeVertical => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
}
