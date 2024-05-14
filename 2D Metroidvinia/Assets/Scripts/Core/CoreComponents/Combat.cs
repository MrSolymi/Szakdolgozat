using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    public void Damage(float amount)
    {
        Debug.LogError(_core.transform.parent.name + " damaged!");
        _core.Stats.DecreaseHealth(amount);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
