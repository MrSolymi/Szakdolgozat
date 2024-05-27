using System;
using Solymi.Core.CoreComponents;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private event Action<Collider2D[]> OnDetectedColliders; 
        
        private CoreComponent<Movement> _movement;

        private Vector2 _offset;
        
        private Collider2D[] _detectedObjects;

        protected override void Start()
        {
            base.Start();
            
            _movement = new CoreComponent<Movement>(Core);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            animationEventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            animationEventHandler.OnAttackAction -= HandleAttackAction;
        }
        
        private void HandleAttackAction()
        {
            _offset.Set(
                transform.position.x + currentAttackData.HitBox.center.x * _movement.Component.FacingDirection,
                transform.position.y + currentAttackData.HitBox.center.y
                );

            _detectedObjects = Physics2D.OverlapBoxAll(_offset, currentAttackData.HitBox.size, 0, data.DetectableLayers);

            if (_detectedObjects.Length == 0) return;
            
            OnDetectedColliders?.Invoke(_detectedObjects);

            foreach (var item in _detectedObjects)
            {
                Debug.LogWarning(item.name);
            }
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