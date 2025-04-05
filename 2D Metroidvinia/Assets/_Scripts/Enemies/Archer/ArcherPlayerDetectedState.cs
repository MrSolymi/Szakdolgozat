using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;
using UnityEngine;

namespace Solymi.Enemies.Archer
{
    public class ArcherPlayerDetectedState : EntityPlayerDetectedState
    {
        private Archer _archer;
        
        public ArcherPlayerDetectedState(Entity entity, EntityData entityData, string animBoolName, Archer archer) : base(entity, entityData, animBoolName)
        {
            _archer = archer;
        }

        public override void Enter()
        {
            base.Enter();
            
            Movement.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (doCloseRangeAction)
            {
                if (Time.time >= _archer.DodgeState.StartTime + EntityData.dodgeCoolDown)
                {
                    StateMachine.ChangeState(_archer.DodgeState);
                }
                else
                {
                    StateMachine.ChangeState(_archer.MeleeAttackState);
                }
            }
            else if (doLongRangeAction)
            {
                StateMachine.ChangeState(_archer.RangedAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                StateMachine.ChangeState(_archer.LookForPlayerState);
            }
        }
    }
}