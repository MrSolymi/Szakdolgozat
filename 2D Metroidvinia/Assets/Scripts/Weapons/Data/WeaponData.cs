using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/WeaponData/Weapon")]
public class WeaponData : ScriptableObject
{
    public int amountOfAttacks { get; protected set; }
    public float[] movementSpeed { get; protected set; }
}