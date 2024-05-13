using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    public void Damage(float damage)
    {
        Debug.LogError(_core.transform.parent.name + " damaged!");
    }
}
