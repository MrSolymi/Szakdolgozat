using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Intermediaries;
using UnityEngine;

namespace Solymi.Enemies.EntityStateMachine
{
    public class Entity : MonoBehaviour
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
    
        [SerializeField] protected EntityData entityData;
        
        public Core.Core Core { get; private set; }
        public EntityStateMachine StateMachine { get; private set; }
        public Animator Animator { get; private set; }

        public virtual void Awake()
        {
            Core = GetComponentInChildren<Core.Core>();
        
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
            Gizmos.DrawLine(CollisionSenses.WallCheck.position, CollisionSenses.WallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * CollisionSenses.WallCheckDistance));
            Gizmos.DrawLine(CollisionSenses.LedgeCheckVertical.position, CollisionSenses.LedgeCheckVertical.position + (Vector3)(Vector2.down * CollisionSenses.WallCheckDistance));
            Gizmos.DrawLine(
                CollisionSenses.PlayerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.minAgroDistance),
                CollisionSenses.PlayerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.maxAgroDistance)
                );
            Gizmos.DrawWireSphere(CollisionSenses.PlayerCheck.position + (Vector3)(Movement.RB.transform.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(CollisionSenses.PlayerCheck.position + (Vector3)(Movement.RB.transform.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(CollisionSenses.PlayerCheck.position + (Vector3)(Movement.RB.transform.right * entityData.maxAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(CollisionSenses.GroundCheck.position, CollisionSenses.GroundCheckRadius);
        }   
    }
}
