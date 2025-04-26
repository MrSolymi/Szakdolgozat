using System;
using Solymi._Scripts.GameManager;
using Solymi.Core.CoreComponents;
using Solymi.Enemies.Data;
using Solymi.Interfaces;
using UnityEngine;

namespace Solymi.Enemies.EntityStateMachine
{
    public abstract class Entity : MonoBehaviour
    {
        private CollisionSenses _collisionSenses;
        protected Movement Movement;
        protected Stats Stats;
    
        [SerializeField] protected EntityData entityData;
        
        [SerializeField] protected UniqueId uniqueId;
        
        public Core.Core Core { get; private set; }
        public EntityStateMachine StateMachine { get; private set; }
        public Animator Animator { get; private set; }

        public virtual void Awake()
        {
            Core = GetComponentInChildren<Core.Core>();
        
            Stats = Core.GetCoreComponent<Stats>();
            Movement = Core.GetCoreComponent<Movement>();
            _collisionSenses = Core.GetCoreComponent<CollisionSenses>();
            
            Animator = GetComponent<Animator>();
        
            StateMachine = new EntityStateMachine();
            if (transform.name != "LittleSlime(Clone)")
                Stats.RespawnPoint.Initialize(FindGameObjectWithUniqueId(uniqueId));
        }

        //private void Start()
        //{
        //    if (EntitySaveTracker.IsDead(uniqueId.Id))
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}

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
            Gizmos.DrawLine(_collisionSenses.WallCheck.position, _collisionSenses.WallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * _collisionSenses.WallCheckDistance));
            Gizmos.DrawLine(_collisionSenses.LedgeCheckVertical.position, _collisionSenses.LedgeCheckVertical.position + (Vector3)(Vector2.down * _collisionSenses.WallCheckDistance));
            Gizmos.DrawLine(
                _collisionSenses.PlayerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.minAgroDistance),
                _collisionSenses.PlayerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.maxAgroDistance)
                );
            Gizmos.DrawWireSphere(_collisionSenses.PlayerCheck.position + (Vector3)(Movement.RB.transform.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(_collisionSenses.PlayerCheck.position + (Vector3)(Movement.RB.transform.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(_collisionSenses.PlayerCheck.position + (Vector3)(Movement.RB.transform.right * entityData.maxAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(_collisionSenses.GroundCheck.position, _collisionSenses.GroundCheckRadius);
        }

        public virtual void ResetAfterSave()
        {
            Stats.RespawnPoint.Respawn(transform);
            Stats.Health.Reset();
            gameObject.SetActive(true);

            var go = GetComponentInChildren<HealthBar>()?.gameObject;

            if (go != null)
            {
                go.SetActive(false);
            }
        }

        private GameObject FindGameObjectWithUniqueId(UniqueId uniqueId)
        {
            var resPoints = GameObject.FindGameObjectsWithTag("EnemyRespawnPoint");

            foreach (var point in resPoints)
            {
                if (point.gameObject.name != uniqueId.Id) continue;
                
                return point;
            }
            
            Debug.LogWarning(name + " could not find its spawn point!");
            
            return null;
        }
    }
}
