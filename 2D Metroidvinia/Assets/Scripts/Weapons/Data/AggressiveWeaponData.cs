using System;
using Solymi.Structs;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/WeaponData/AggressiveWeapon")]
public class AggressiveWeaponData : WeaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails
    {
        get => attackDetails;
        private set => attackDetails = value;
    }

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;
        
        movementSpeed = new float[amountOfAttacks];

        for (var i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
