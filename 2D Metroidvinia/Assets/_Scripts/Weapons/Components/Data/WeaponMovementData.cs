using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components.Data
{
    public class WeaponMovementData : WeaponComponentData<AttackMovement>
    {
        // [field: SerializeField] public AttackMovement[] AttackData { get; private set; }
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(WeaponMovement);
        }
    }
}