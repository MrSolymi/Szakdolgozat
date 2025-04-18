using System;
using Solymi.Core.CoreComponents;
using Solymi.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Solymi.Projectiles
{
    public class ArcherProjectile : MonoBehaviour
    {
        private bool _isGravityEnabled, _hasHitGround;
        private float _speed, _travelDistance, _damage, _xStartPosition;
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField]
        private float gravity, damageRadius, kockbackStrength;
        [SerializeField]
        private LayerMask whatIsGround, whatIsPlayer;
        [SerializeField]
        private Transform damagePosition;
        [SerializeField]
        private Vector2 knockbackAngle;
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _rigidbody2D.gravityScale = 0f;
            _isGravityEnabled = false;
            _rigidbody2D.velocity = transform.right * _speed;
            
            _xStartPosition = transform.position.x;
        }

        private void Update()
        {
            if (!_hasHitGround)
            {
                if (_isGravityEnabled)
                {
                    var angle = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_hasHitGround)
            {
                var playerHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
                var groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

                if (playerHit)
                {
                    if (playerHit.GetComponentInChildren<KnockBackReceiver>().TryGetComponent(out IKnockBackable knockBackable))
                    {
                        knockBackable.KnockBack(knockbackAngle, kockbackStrength, (int)(playerHit.transform.position.x - _xStartPosition));
                    }
                    
                    if (playerHit.GetComponentInChildren<DamageReceiver>().TryGetComponent(out IDamageable damageable))
                    {
                        damageable.Damage(_damage);
                        Destroy(gameObject);
                    }
                }
                

                //TODO: Implement knockback on arrow hit
                //if (hitDamage.GetComponentInChildren<KnockBackReceiver>().TryGetComponent(out IKnockBackable knockBackable))
                //{
                //    knockBackable.KnockBack();
                //}

                if (groundHit)
                {
                    _hasHitGround = true;
                    _rigidbody2D.gravityScale = 0f;
                    _rigidbody2D.velocity = Vector2.zero;
                }
                
                if (Mathf.Abs(_xStartPosition - transform.position.x) >= _travelDistance && !_isGravityEnabled)
                {
                    _isGravityEnabled = true;
                    _rigidbody2D.gravityScale = gravity;
                }
            }
        }
        
        public void FireProjectile(float speed, float travelDistance, float damage)
        {
            _speed = speed;
            _travelDistance = travelDistance;
            _damage = damage;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
        }
    }
}