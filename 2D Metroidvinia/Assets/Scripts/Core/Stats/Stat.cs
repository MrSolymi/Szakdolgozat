using System;
using UnityEngine;
using UnityEngine.UI;

namespace Solymi.Core.Stats
{
    [Serializable] public class Stat
    {
        public event Action OnCurrentValueZero;
        
        [field: SerializeField] public float MaxValue { get; private set; }
        
        private float _currentValue;
        public float CurrentValue
        {
            get => _currentValue;
            private set
            {
                _currentValue = Mathf.Clamp(value, 0.0f, MaxValue);
                if (_currentValue <= 0)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        }
        
        public void Initialize() => CurrentValue = MaxValue;
        
        public void Reset() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;
    }
}