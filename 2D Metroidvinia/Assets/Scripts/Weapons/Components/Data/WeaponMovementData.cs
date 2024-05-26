using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components.Data
{
    public class WeaponMovementData : WeaponComponentData
    {
        [field: SerializeField] public AttackMovement[] AttackData { get; private set; }
    }
}