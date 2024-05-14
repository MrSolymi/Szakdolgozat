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
        
        foreach (var item in _detectedDamageables)
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
