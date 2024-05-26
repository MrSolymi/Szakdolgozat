using System;
using UnityEngine;

namespace Solymi.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinished, OnStartMovement, OnStopMovement;
        
        private void AnimationFinishTrigger() => OnFinished?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
    }
}