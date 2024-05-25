using System;
using UnityEngine;

namespace Solymi.Utilities
{
    public class Timer
    {
        public event Action OnTimerEnd;
        
        private float _startTime, _duration, _targetTime;
        private bool _isActive;

        public Timer(float duration)
        {
            _duration = duration;
        }
        
        public void StartTimer()
        {
            _startTime = Time.time;
            _targetTime = _startTime + _duration;
            
            _isActive = true;
        }

        public void StopTimer() => _isActive = false;

        public void Tick()
        {
            if (!_isActive) return;
            
            if (Time.time >= _targetTime)
            {
                OnTimerEnd?.Invoke();
                StopTimer();
            }
        }
    }
}