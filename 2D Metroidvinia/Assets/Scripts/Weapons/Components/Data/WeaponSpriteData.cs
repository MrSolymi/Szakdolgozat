using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components.Data
{
    public class WeaponSpriteData : WeaponComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; private set; }
    }
}