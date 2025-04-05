using Solymi.Interfaces;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<Core>();

            if (!core)
            {
                Debug.LogError("There is no Core on the parent object of " + this + " component.");
            }
            core.AddComponent(this);
        }

        public virtual void LogicUpdate()
        {
        
        }
    }

    /*
    public class CoreComponent<T> where T : CoreComponent
    {
        private Core _core;
        private T _component;
        
        public T Component => _component ? _component : _core.GetCoreComponent(ref _component);

        public CoreComponent(Core core)
        {
            if (core == null)
            {
                Debug.LogWarning( $"Core is null for component {typeof(T)}!");
            }
            else
            {
                _core = core;
            }
        }
    }
    */
}
