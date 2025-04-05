using Solymi.Weapons.Components.Data.AttackData;

namespace Solymi.Weapons.Components.Data
{
    public class WeaponKnockBackData : WeaponComponentData<AttackKnockBack>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(WeaponKnockBack);
        }
    }
}