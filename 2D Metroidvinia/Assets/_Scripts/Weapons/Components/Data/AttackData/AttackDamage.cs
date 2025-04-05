using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    [Serializable] public class AttackDamage : AttackData
    {
        [field: SerializeField] public float DamageAmount { get; private set; }
    }
}