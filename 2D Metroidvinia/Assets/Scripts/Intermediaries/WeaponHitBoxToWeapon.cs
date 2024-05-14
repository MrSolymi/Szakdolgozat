using System;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponHitBoxToWeapon : MonoBehaviour
{
    private AggressiveWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogError("OnTriggerEnter2D " + other.gameObject.name);
        _weapon.AddToDetectedDamageables(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.LogError("OnTriggerExit2D");
        _weapon.RemoveFromDetectedDamageables(other);
    }
}
