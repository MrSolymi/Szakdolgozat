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
}
