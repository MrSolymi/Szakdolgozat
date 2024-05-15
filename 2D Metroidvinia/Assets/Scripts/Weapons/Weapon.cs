using System;
using UnityEngine;

namespace Solymi.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action OnExit;
        
        private Animator _animator;
        private GameObject _baseGameObject;

        private AnimationEventHandler _eventHandler;
        public void Enter()
        {
            Debug.Log($"{transform.name} entered");
            
            _animator.SetBool("active", true);
        }

        private void Exit()
        {
            _animator.SetBool("active", false);
            
            OnExit?.Invoke();
        }

        private void Awake()
        {
            _baseGameObject = transform.Find("Base").gameObject;
            _animator = _baseGameObject.GetComponent<Animator>();
            
            _eventHandler = _baseGameObject.GetComponent<AnimationEventHandler>();
        }

        private void OnEnable()
        {
            _eventHandler.OnFinished += Exit;
        }

        private void OnDisable()
        {
            _eventHandler.OnFinished -= Exit;
        }
    }
}