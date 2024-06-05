using System;
using Solymi.Core.CoreComponents;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedColliders; 
        
        private Movement _movement;

        private Vector2 _offset;
        
        private Collider2D[] _detectedObjects;

        protected override void Start()
        {
            base.Start();
            
            //_movement = new CoreComponent<Movement>(Core);
            _movement = Core.GetCoreComponent<Movement>();
            
            animationEventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            animationEventHandler.OnAttackAction -= HandleAttackAction;
        }
        
        private void HandleAttackAction()
        {
            _offset.Set(
                transform.position.x + currentAttackData.HitBox.center.x * _movement.FacingDirection,
                transform.position.y + currentAttackData.HitBox.center.y
                );

            _detectedObjects = Physics2D.OverlapBoxAll(_offset, currentAttackData.HitBox.size, 0, data.DetectableLayers);

            if (_detectedObjects.Length == 0) return;
            
            OnDetectedColliders?.Invoke(_detectedObjects);
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null) return;

            foreach (var item in data.AttackData)
            {
                if (item.Debug)
                {
                    Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
                }
            }
        }
    }
}