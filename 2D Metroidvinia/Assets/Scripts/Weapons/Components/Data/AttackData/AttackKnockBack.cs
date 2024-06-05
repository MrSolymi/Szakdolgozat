using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    [Serializable] public class AttackKnockBack : AttackData
    {
        [field: SerializeField] public float KnockBackStrength { get; private set; }
        [field: SerializeField] public Vector2 KnockBackAngle { get; private set; }
    }
}