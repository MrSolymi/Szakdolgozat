using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    private Stats Stats => _stats ? _stats : _core.GetCoreComponent(ref _stats);
    private Stats _stats;
    
    public void Damage(float amount)
    {
        Debug.LogError(_core.transform.parent.name + " damaged!");
        Stats.DecreaseHealth(amount);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
