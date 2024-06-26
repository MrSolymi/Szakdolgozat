using Solymi.Weapons.Components.Data.AttackData;

namespace Solymi.Weapons.Components.Data
{
    public class WeaponDamageData : WeaponComponentData<AttackDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(WeaponDamage);
        }
    }
}