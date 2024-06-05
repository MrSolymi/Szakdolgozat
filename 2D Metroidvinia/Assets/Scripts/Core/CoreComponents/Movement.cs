using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB { get; private set; }
        public int FacingDirection { get; private set; }
        public Vector2 CurrentVelocity { get; private set; }
        public bool CanSetVelocity { get; set; } = true;
        
        private Vector2 _workspace;
        

        protected override void Awake()
        {
            base.Awake();
        
            RB = GetComponentInParent<Rigidbody2D>();
        
            FacingDirection = 1;
            CanSetVelocity = true;
        }
    
        public override void LogicUpdate()
        {
            // base.LogicUpdate(); // This is empty
        
            CurrentVelocity = RB.velocity;
        }
    
        public void SetVelocityZero()
        {
            // RB.velocity = Vector2.zero;
            // CurrentVelocity = Vector2.zero;
            _workspace.Set(Vector2.zero.x, Vector2.zero.y);
            FinalizeVelocity();
        }
        public void SetVelocityX(float velocity)
        {
            _workspace.Set(velocity, CurrentVelocity.y);
            RB.velocity = _workspace;
            CurrentVelocity = _workspace;
            FinalizeVelocity();
        }
    
        public void SetVelocityY(float velocity)
        {
            _workspace.Set(CurrentVelocity.x, velocity);
            RB.velocity = _workspace;
            CurrentVelocity = _workspace;
            FinalizeVelocity();
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            RB.velocity = _workspace;
            CurrentVelocity = _workspace;
            FinalizeVelocity();
        }
        public void SetDashVelocity(float velocity, int direction)
        {
            _workspace.Set(velocity * direction , 0);
            RB.velocity = _workspace;
            CurrentVelocity = _workspace;
            FinalizeVelocity();
        }
        
        private void FinalizeVelocity()
        {
            if (CanSetVelocity)
            {
                RB.velocity = _workspace;
                CurrentVelocity = _workspace;
            }
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
}
