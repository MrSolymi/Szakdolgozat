using System;
using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    [Serializable] public class AttackSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}