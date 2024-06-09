using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    [Serializable] public class AttackPoiseDamage : AttackData
    {
        [field: SerializeField] public float PoiseDamage { get; private set; }
    }
}