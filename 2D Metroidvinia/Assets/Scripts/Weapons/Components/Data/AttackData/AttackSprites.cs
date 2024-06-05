using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    /// <summary> Represents the sprite data for an attack. </summary>
    [Serializable] public class AttackSprites : AttackData
    {
        /// <summary> The sprites associated with the attack. </summary>
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}