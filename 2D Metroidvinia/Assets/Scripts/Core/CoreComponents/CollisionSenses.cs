using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck { get => groundCheck; set => groundCheck = value; }
    [SerializeField] private Transform groundCheck;
    
    public Transform WallCheck { get => wallCheck; set => wallCheck = value; }
    [SerializeField] private Transform wallCheck;
    
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

    public bool Ground => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    
    public bool Wall => Physics2D.Raycast(wallCheck.position, Vector2.right * _core.Movement.FacingDirection ,wallCheckDistance, whatIsGround);

    
}
