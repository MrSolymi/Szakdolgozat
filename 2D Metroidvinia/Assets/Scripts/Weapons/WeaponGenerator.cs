using System;
using System.Collections.Generic;
using System.Linq;
using Solymi.Weapons.Components;
using Solymi.Weapons.Data;
using UnityEngine;

namespace Solymi.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponData weaponData;
        
        private List<WeaponComponent> _componentsOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> _componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> _componentDependencies = new List<Type>();

        private void Start()
        {
            GenerateWeapon(weaponData);
        }
        
        [ContextMenu("test generate weapon")] private void Testing()
        {
            GenerateWeapon(weaponData);
        }

        public void GenerateWeapon(WeaponData data)
        {
            weapon.SetWeaponData(data);
            
            _componentsOnWeapon.Clear();
            _componentsAddedToWeapon.Clear();
            _componentDependencies.Clear();

            _componentsOnWeapon = GetComponents<WeaponComponent>().ToList();
            _componentDependencies = data.GetAllDependencies();

            foreach (var dependency in _componentDependencies)
            {
                if (_componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency) != null)
                {
                    continue;
                }
                
                var weaponComponent = _componentsOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if (weaponComponent == null)
                {
                    weaponComponent =  gameObject.AddComponent(dependency) as WeaponComponent;
                }
                
                weaponComponent.Init();
                
                _componentsAddedToWeapon.Add(weaponComponent);
            }
            
            var componentsToRemove = _componentsOnWeapon.Except(_componentsAddedToWeapon).ToList();
            
            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
        }
    }
}