using Solymi.Enemies.Data;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.EntityStates;

namespace Solymi.Enemies.Enemy1
{
    public class Enemy1MoveState : EntityMoveState
    {
        private Enemy1 _enemy;
    
        public Enemy1MoveState(Entity entity, EntityData entityData, string animBoolName, Enemy1 enemy) : base(entity, entityData, animBoolName)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isDetectingWall || !isDetectingLedge)
            {
                _enemy.idleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_enemy.idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}
