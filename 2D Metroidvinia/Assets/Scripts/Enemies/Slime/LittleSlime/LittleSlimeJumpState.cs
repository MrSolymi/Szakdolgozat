using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;
using UnityEngine;

namespace Solymi.Enemies.Slime.LittleSlime
{
    public class LittleSlimeJumpState : EntityJumpState
    {
        private LittleSlime _slime;
        public LittleSlimeJumpState(Entity entity, EntityData entityData, string animBoolName, LittleSlime slime) : base(entity, entityData, animBoolName)
        {
            _slime = slime;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            if (!isGrounded) return;
            
            Movement.SetVelocityX(EntityData.movementSpeed * Movement.FacingDirection);
            Movement.RB.AddForce(Vector2.up * Random.Range(EntityData.jumpForce-2f, EntityData.jumpForce+2f) , ForceMode2D.Impulse);
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Movement.CurrentVelocity.y <= 0 && isGrounded)
            {
                if (isPlayerInMinAgroRange)
                {
                    StateMachine.ChangeState(_slime.JumpPrepState);
                }
                else
                {
                    StateMachine.ChangeState(_slime.JumpLandState);
                }
                
                //StateMachine.ChangeState(_slime.JumpLandState);
            }
            
            _slime.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        }
    }
}