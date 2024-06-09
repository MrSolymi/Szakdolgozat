using System;
using Solymi.Interfaces;
using Solymi.Utilities;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        [SerializeField] public float knockBackMaxDuration = 0.3f;
        
        private bool _isKnockBackActive;
        private float _knockBackStartTime;
        
        //private Stats _stats;
        private Movement _movement;
        private CollisionSenses _collisionSenses;

        protected override void Awake()
        {
            base.Awake();
            
            //_stats = core.GetCoreComponent<Stats>();
            _movement = core.GetCoreComponent<Movement>();
            _collisionSenses = core.GetCoreComponent<CollisionSenses>();
            
            //_stats = new CoreComponent<Stats>(core);
            //_movement = new CoreComponent<Movement>(core);
            //_collisionSenses = new CoreComponent<CollisionSenses>(core);
        }
        
        public override void LogicUpdate()
        {
            CheckKnockBack();
        }
        private void CheckKnockBack()
        {
            // if (_isKnockBackActive && ((_movement.CurrentVelocity.y <= 0.01 && _collisionSenses.Ground) || Time.time >= _knockBackStartTime + knockBackMaxDuration))
            // {
            //     _isKnockBackActive = false;
            //     _movement.CanSetVelocity = true;
            // }

            if (!_isKnockBackActive) return;
            
            
            if (_collisionSenses.Ground && Mathf.Abs(_movement.CurrentVelocity.y) <= 0.01)
            {
                _isKnockBackActive = false;
                _movement.CanSetVelocity = true;
                _movement.SetVelocityZero();
            }
            else if (Time.time >= _knockBackStartTime + knockBackMaxDuration)
            {
                _isKnockBackActive = false;
                _movement.CanSetVelocity = true;
            }
        }

        public void KnockBack(Vector2 knockBackAngle, float knockBackStrength, int direction)
        {
            _movement.SetVelocity(knockBackStrength, knockBackAngle, direction);
            _movement.CanSetVelocity = false;
            _isKnockBackActive = true;
            _knockBackStartTime = Time.time;
            Debug.LogWarning(_movement.transform.parent.parent.name + " Knocked back");
        }
    }
}
