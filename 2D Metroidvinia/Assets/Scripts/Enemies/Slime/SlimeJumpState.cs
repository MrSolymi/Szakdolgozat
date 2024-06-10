using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;
using UnityEngine;

namespace Solymi.Enemies.Slime
{
    public class SlimeJumpState : EntityJumpState
    {
        private Slime _slime;
        public SlimeJumpState(Entity entity, EntityData entityData, string animBoolName, Slime slime) : base(entity, entityData, animBoolName)
        {
            _slime = slime;
        }

        public override void Enter()
        {
            base.Enter();
            
            Movement.SetVelocityX(EntityData.movementSpeed * Movement.FacingDirection);
            Movement.RB.AddForce(Vector2.up * EntityData.jumpForce, ForceMode2D.Impulse);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
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