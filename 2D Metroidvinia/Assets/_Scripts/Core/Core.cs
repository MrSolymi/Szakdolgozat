using System.Collections.Generic;
using System.Linq;
using Solymi.Core.CoreComponents;
using UnityEngine;

namespace Solymi.Core
{
    public class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();

        #region OldSetup
        /*
        public Movement Movement
        {
            get => GenericNotImplementedError<Movement>.TryGet(_movement, transform.parent.name);
            private set => _movement = value;
        }
        private Movement _movement;

        public CollisionSenses CollisionSenses
        {
            get => GenericNotImplementedError<CollisionSenses>.TryGet(_collisionSenses, transform.parent.name);
            private set => _collisionSenses = value;
        }
        private CollisionSenses _collisionSenses;
        
        public Combat Combat
        {
            get => GenericNotImplementedError<Combat>.TryGet(_combat, transform.parent.name);
            private set => _combat = value;
        }
        private Combat _combat;
        
        public Stats Stats
        {
            get => GenericNotImplementedError<Stats>.TryGet(_stats, transform.parent.name);
            private set => _stats = value;
        }
        private Stats _stats;
    
        private void Awake()
        {
            // Movement = GetComponentInChildren<Movement>();
            // CollisionSenses = GetComponentInChildren<CollisionSenses>();
            // Combat = GetComponentInChildren<Combat>();
            // Stats = GetComponentInChildren<Stats>();
        }
        */
        #endregion
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
