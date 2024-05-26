using System;
using System.Collections.Generic;
using System.Linq;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Data;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Solymi.Weapons
{
    /// <summary>
    /// Custom editor for the WeaponData class.
    /// Allows users to add various types of WeaponComponentData to a WeaponData instance via the Unity Inspector.
    /// </summary>
    [CustomEditor(typeof(WeaponData))] public class WeaponDataEditor : Editor
    {
        /// <summary>
        /// A static list holding the types of WeaponComponentData components available for the weapon.
        /// </summary>
        private static List<Type> _dataComponentTypes = new List<Type>();
        
        /// <summary>
        /// A reference to the WeaponData instance being edited.
        /// </summary>
        private WeaponData _weaponData;
        
        /// <summary>
        /// Called when the editor is enabled.
        /// Initializes the _weaponData field with the target object cast to WeaponData.
        /// </summary>
        private void OnEnable()
        {
            _weaponData = target as WeaponData;
        }
        
        /// <summary>
        /// Overrides the default OnInspectorGUI method to add custom GUI elements to the inspector.
        /// Calls base.OnInspectorGUI() to ensure the default inspector GUI is drawn.
        /// Iterates over _dataComponentTypes and adds a button for each type.
        /// When a button is clicked, an instance of the corresponding WeaponComponentData type is created and added to the WeaponData instance.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var dataComponentType in _dataComponentTypes)
            {
                if (GUILayout.Button(dataComponentType.Name))
                {
                    var componentData = Activator.CreateInstance(dataComponentType) as WeaponComponentData;
                    if (componentData == null) return;
                    
                    _weaponData.AddComponentData(componentData);
                }
            }
        }

        /// <summary>
        /// A static method marked with the [DidReloadScripts] attribute, which is called after scripts are recompiled.
        /// Retrieves all types from all assemblies in the current application domain.
        /// Filters these types to find subclasses of WeaponComponentData that are non-generic classes.
        /// Updates the _dataComponentTypes list with the filtered types.
        /// </summary>
        [DidReloadScripts] private static void OnRecompile()
        {
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

            var filteredTypes = types.Where(
                type => type.IsSubclassOf(typeof(WeaponComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );
            
            _dataComponentTypes = filteredTypes.ToList();
        }
    }
}