using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected AggressiveWeaponData aggressiveWeaponData;
    
    private List<IDamageable> _detectedDamageables = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(AggressiveWeaponData))
        {
            aggressiveWeaponData = (AggressiveWeaponData) weaponData;
        }
        else
        {
            Debug.LogError("Weapon data is not of type AggressiveWeaponData");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        
        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];
        
        //InvalidOperationException: Collection was modified; enumeration operation may not execute.System.Collections.Generic.List`1+Enumerator[T].MoveNextRare ()
        //Solution was to create a copy of the list and iterate over it in the following line.
        var targetsToDamage = new List<IDamageable>(_detectedDamageables);
        
        foreach (var item in targetsToDamage)
        {
            item.Damage(details.damageAmount);
        }
    }
    
    public void AddToDetectedDamageables(Collider2D collision)
    {
        //Debug.LogError("AddToDetectedDamageables");
        
        var damageable = collision.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            //Debug.LogError("Added!");
            _detectedDamageables.Add(damageable);
        }
    }
    
    public void RemoveFromDetectedDamageables(Collider2D collision)
    {
        //Debug.LogError("RemoveFromDetectedDamageables");
        
        var damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            //Debug.LogError("Removed!");
            _detectedDamageables.Remove(damageable);
        }
    }
}
