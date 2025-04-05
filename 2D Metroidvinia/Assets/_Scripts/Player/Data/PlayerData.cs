using UnityEngine;

namespace Solymi.Player.Data
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Stats")]
        public float gravity = 5;
    
        [Header("Move State")]
        public float movementVelocity = 10f;
    
        [Header("Jump State")]
        public float jumpVelocity = 15f;
        public int amountOfJumps = 1;
        public float jumpHeightMultiplier = 0.5f;
    
        [Header("Wall Jump State")]
        public float wallJumpVelocity = 15f;
        public float wallJumpTime = 0.35f;
        public Vector2 wallJumpAngle = new Vector2(1, 2);

        [Header("Ledge Climb State")] 
        public Vector2 ledgeClimbStartOffset;
        public Vector2 ledgeClimbStopOffset;
    
        [Header("In Air State")]
        public float coyoteTime = 0.1f;
    
        [Header("Wall Slide State")]
        public float wallSlideVelocity = 3.0f;
    
        [Header("Dash State")]
        public float dashCoolDown = 0.5f;
        public float dashTime = 0.15f;
        public float dashVelocity = 30f;
        //public float dashDrag = 10f;
        public float distanceBetweenAfterImages = 0.5f;
    }
}
