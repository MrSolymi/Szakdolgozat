using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
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

    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : WeaponComponentData<T2> where T2 : AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;
        protected override void Awake()
        {
            base.Awake();
            
            data = weapon.WeaponData.GetWeaponComponentData<T1>();
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
        }
    }
}