using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    [Serializable] public class AttackActionHitBox : AttackData
    {
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}