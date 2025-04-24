using System.Collections.Generic;
using System.Linq;
using Solymi.Core.CoreComponents;
using UnityEngine;

namespace Solymi.Core
{
    public class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();
        
        public void LogicUpdate()
        {
            foreach (var component in _coreComponents)
            {
                component.LogicUpdate();
            }
        }
    
        public void AddComponent(CoreComponent component)
        {
            if (!_coreComponents.Contains(component))
            {
                _coreComponents.Add(component);
            }
        }
    
        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var component = _coreComponents.OfType<T>().FirstOrDefault();

            if (component) return component;
        
            component = GetComponentInChildren<T>();

            if (component) return component;
        
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }
    
        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}
