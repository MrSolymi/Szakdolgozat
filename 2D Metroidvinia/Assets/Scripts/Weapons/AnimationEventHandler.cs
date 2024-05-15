using System;
using UnityEngine;

namespace Solymi.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinished;
        
        private void AnimationFinishTrigger() => OnFinished?.Invoke();
    }
}