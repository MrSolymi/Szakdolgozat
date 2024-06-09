using UnityEngine;
using UnityEngine.Serialization;

namespace Solymi.Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
    public class EntityData : ScriptableObject
    {
        [Header("Base Settings")]
        public float movementSpeed = 3.0f;
        
        public float minAgroDistance = 2.0f;
        public float maxAgroDistance = 4.0f;
        
        public float timeBeforeLongRangeAction = 1.0f;
        [Header("Idle State")]
        public float minIdleTime = 1.0f;
        public float maxIdleTime = 2.0f;
        
        [Header("Charge State")]
        public float chargeTime = 2.0f;
        public float chargeSpeed = 6.0f;
        
        [Header("Look For Player State")]
        public int amountOfTurns = 2;
        public float timeBetweenTurns = 0.5f;
        
        [Header("Attack State")]
        public float closeRangeActionDistance = 1.0f;
        public float longRangeActionDistance = 3.0f;
        public float meleeAttackRadius = 0.5f;
        public float meleeAttackDamage = 10.0f;
        
        [FormerlySerializedAs("stunTime")] [Header("Stun State")]
        public float stunDuration = 3.0f;
        
        [Header("KnockBacks")]
        public float knockBackStrength = 10.0f;
        public float knockBackTime = 0.2f;
        public Vector2 knockBackAngle = new Vector2(1.0f, 1.0f);
        
        public float rangedKnockBackStrength = 5.0f;
        public float rangedKnockBackTime = 0.2f;
        public Vector2 rangedKnockBackAngle = new Vector2(1.0f, 1.0f);
    }
}
