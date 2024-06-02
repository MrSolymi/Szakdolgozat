using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Solymi.Weapons.Components.Data
{
    [Serializable] public abstract class WeaponComponentData
    {
        [SerializeField, HideInInspector] private string name;
        
        public Type ComponentDependency { get; protected set; }

        public void SetComponentName()
        {
            name = GetType().Name;
        }

        public WeaponComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }
        
        public virtual void SetAttackDataNames() { }
        public virtual void InitializeAttackData(int numberOfAttacks) { }
        
        protected abstract void SetComponentDependency();
    }
    
    [Serializable] public abstract class WeaponComponentData<T> : WeaponComponentData where T : AttackData.AttackData
    {
        [SerializeField] private T[] attackData;
        public T[] AttackData { get => attackData; private set => attackData = value; }

        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();
            
            for (var i = 0; i < attackData.Length; i++)
            {
                attackData[i].SetAttackName(i + 1);
            }
        }

        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);
            
            var oldLenght = attackData != null ? attackData.Length : 0;
            if (oldLenght == numberOfAttacks)
            {
                return;
            }
            Array.Resize(ref attackData, numberOfAttacks);

            if (oldLenght < numberOfAttacks)
            {
                for (var i = oldLenght; i < attackData.Length; i++)
                {
                    var newObject = Activator.CreateInstance(typeof(T)) as T;
                    attackData[i] = newObject;
                }
            }
            SetAttackDataNames();
        }
    }
}