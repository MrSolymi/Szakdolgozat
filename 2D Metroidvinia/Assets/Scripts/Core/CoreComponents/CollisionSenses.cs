using Solymi.Enemies.Data;
using Solymi.Generics;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class CollisionSenses : CoreComponent
    {
        [SerializeField]
        private EntityData entityData;
        
        private Movement Movement => _movement ? _movement : core.GetCoreComponent(ref _movement);
        private Movement _movement;
    
        [SerializeField] private Transform groundCheck;
        public Transform GroundCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
            private set => groundCheck = value;
        }

        [SerializeField] private Transform wallCheck;
        public Transform WallCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
            private set => wallCheck = value;
        }
    
        [SerializeField] private Transform ledgeCheckHorizontal;
        public Transform LedgeCheckHorizontal
        {
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
            private set => ledgeCheckHorizontal = value;
        }

        [SerializeField] private Transform ledgeCheckVertical;
        public Transform LedgeCheckVertical
        {
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
            private set => ledgeCheckVertical = value;
        }
        
        [SerializeField] private Transform playerCheck;
        public Transform PlayerCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(playerCheck, core.transform.parent.name);
            private set => playerCheck = value;
        }

        public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
        [SerializeField] private float groundCheckRadius;
    
        public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
        [SerializeField] private float wallCheckDistance;
    
        public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
        [SerializeField] private LayerMask whatIsGround;
        
        public LayerMask WhatIsPlayer { get => whatIsPlayer; set => whatIsPlayer = value; }
        [SerializeField] private LayerMask whatIsPlayer;

        // public bool CheckIfGrounded()
        // {
        //     return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        // }

        public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    
        public bool Wall => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection ,wallCheckDistance, whatIsGround);
    
        public bool WallBackwards => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection ,wallCheckDistance, whatIsGround);
    
        public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
    
        public bool LedgeVertical => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
        
        public bool PlayerInMinAgroRange => Physics2D.Raycast(PlayerCheck.position, Vector2.right * Movement.FacingDirection, entityData.minAgroDistance, whatIsPlayer);
        
        public bool PlayerInMaxAgroRange => Physics2D.Raycast(PlayerCheck.position, Vector2.right * Movement.FacingDirection, entityData.maxAgroDistance, whatIsPlayer);
        
        public bool PlayerInCloseRangeAction => Physics2D.Raycast(PlayerCheck.position, Vector2.right * Movement.FacingDirection, entityData.closeRangeActionDistance, whatIsPlayer);
        
        public bool PlayerInLongRangeAction => Physics2D.Raycast(PlayerCheck.position, Vector2.right * Movement.FacingDirection, entityData.longRangeActionDistance, whatIsPlayer);
    }
}
