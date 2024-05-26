using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data
{
    [Serializable] public class WeaponComponentData
    {
        
    }
    
    [Serializable] public class WeaponComponentData<T> : WeaponComponentData where T : AttackData.AttackData
    {
        [field: SerializeField] public T[] AttackData { get; private set; }
    }
}