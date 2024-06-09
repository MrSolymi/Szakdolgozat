using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Intermediaries;
using UnityEngine;

namespace Solymi.Enemies.EntityStateMachine
{
    public class Entity : MonoBehaviour
    {
        private Movement _movement;

        private CollisionSenses _collisionSenses;
    
        [SerializeField] protected EntityData entityData;
        
        public Core.Core Core { get; private set; }
        public EntityStateMachine StateMachine { get; private set; }
        public Animator Animator { get; private set; }

        protected Stats Stats;

        public virtual void Awake()
        {
            Core = GetComponentInChildren<Core.Core>();
        
            Stats = Core.GetCoreComponent<Stats>();
            _movement = Core.GetCoreComponent<Movement>();
            _collisionSenses = Core.GetCoreComponent<CollisionSenses>();
            
            Animator = GetComponent<Animator>();
        
            StateMachine = new EntityStateMachine();
        }

        public virtual void Update()
        {
            Core.LogicUpdate();
            
            StateMachine.CurrentState.LogicUpdate();
        }

        public virtual void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    
        // public virtual void Testing()
        // {
        //     Debug.Log(Core.Movement.RB==null);
        // }
        
    
        public virtual void OnDrawGizmos()
        {
            if (!Core) return;
            Gizmos.DrawLine(_collisionSenses.WallCheck.position, _collisionSenses.WallCheck.position + (Vector3)(Vector2.right * _movement.FacingDirection * _collisionSenses.WallCheckDistance));
            Gizmos.DrawLine(_collisionSenses.LedgeCheckVertical.position, _collisionSenses.LedgeCheckVertical.position + (Vector3)(Vector2.down * _collisionSenses.WallCheckDistance));
            Gizmos.DrawLine(
                _collisionSenses.PlayerCheck.position + (Vector3)(Vector2.right * _movement.FacingDirection * entityData.minAgroDistance),
                _collisionSenses.PlayerCheck.position + (Vector3)(Vector2.right * _movement.FacingDirection * entityData.maxAgroDistance)
                );
            Gizmos.DrawWireSphere(_collisionSenses.PlayerCheck.position + (Vector3)(_movement.RB.transform.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(_collisionSenses.PlayerCheck.position + (Vector3)(_movement.RB.transform.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(_collisionSenses.PlayerCheck.position + (Vector3)(_movement.RB.transform.right * entityData.maxAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(_collisionSenses.GroundCheck.position, _collisionSenses.GroundCheckRadius);
        }   
    }
}
