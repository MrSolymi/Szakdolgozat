using Solymi.Weapons.Components.Data.AttackData;

namespace Solymi.Weapons.Components.Data
{
    public class WeaponPoiseDamageData : WeaponComponentData<AttackPoiseDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(WeaponPoiseDamage);
        }
    }
}