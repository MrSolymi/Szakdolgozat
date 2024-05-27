using UnityEngine;

namespace Solymi.Weapons.Components.Data.AttackData
{
    public class AttackData
    {
        [SerializeField, HideInInspector] private string _name;

        public void SetAttackName(int i)
        {
            _name = $"Attack {i}";
        }
    }
}