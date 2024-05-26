using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    /// <summary> Represents the movement data for an attack. </summary>
    [Serializable] public class AttackMovement : AttackData
    {
        /// <summary> The direction of the attack movement. </summary>
        [field: SerializeField] public Vector2 Direction {get; private set;}
        
        /// <summary> The velocity of the attack movement. </summary>
        [field: SerializeField] public float Velocity {get; private set;}
    }
}