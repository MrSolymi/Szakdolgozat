using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollisionSenses : CoreComponent
{
    private Movement Movement => _movement ? _movement : _core.GetCoreComponent(ref _movement);
    private Movement _movement;
    
    [SerializeField] private Transform groundCheck;
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, _core.transform.parent.name);
        private set => groundCheck = value;
    }

    [SerializeField] private Transform wallCheck;
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, _core.transform.parent.name);
        private set => wallCheck = value;
    }
    
    [SerializeField] private Transform ledgeCheckHorizontal;
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, _core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }

    [SerializeField] private Transform ledgeCheckVertical;
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, _core.transform.parent.name);
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
    
    public bool Wall => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection ,wallCheckDistance, whatIsGround);
    
    public bool WallBackwards => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection ,wallCheckDistance, whatIsGround);
    
    public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
    
    public bool LedgeVertical => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
}
