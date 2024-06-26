using System;
using System.Collections.Generic;
using System.Linq;
using Solymi.Weapons.Components.Data;
using UnityEngine;

namespace Solymi.Weapons.Data
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks {get; private set;}
        
        [field: SerializeReference] public List<WeaponComponentData> WeaponComponentDatas { get; private set; }

        public T GetWeaponComponentData<T>()
        {
            return WeaponComponentDatas.OfType<T>().FirstOrDefault();
        }
        
        public List<Type> GetAllDependencies()
        {
            return WeaponComponentDatas.Select(component => component.ComponentDependency).ToList();
        }
        
        public void AddComponentData(WeaponComponentData data)
        {
            if (WeaponComponentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            {
                return;
            }
            
            WeaponComponentDatas.Add(data);
        }


        // [ContextMenu("Add Sprite Data")]
        // private void AddSpriteData() => WeaponComponentDatas.Add(new WeaponSpriteData());
        //
        // [ContextMenu("Add Movement Data")]
        // private void AddMovementData() => WeaponComponentDatas.Add(new WeaponMovementData());
    }
}