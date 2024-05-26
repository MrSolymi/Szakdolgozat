using UnityEngine;

namespace Solymi.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        //protected AnimationEventHandler AnimationEventHandler => weapon.EventHandler;
        protected AnimationEventHandler animationEventHandler;
        protected Core.Core Core => weapon.Core;
        
        protected bool isAttackActive;
        
        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();

            animationEventHandler = GetComponentInChildren<AnimationEventHandler>();
        }
        
        protected virtual void OnEnable()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }
        
        protected virtual void OnDisable()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
        
        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }
        
        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }
    }
}