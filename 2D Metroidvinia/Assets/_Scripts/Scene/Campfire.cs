using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Solymi._Scripts.Scene
{
    public class Campfire : MonoBehaviour
    {
        [SerializeField] private string campfireSceneName; 
        
        private Animator _animator;
        private PlayerInput _playerInput;
        private bool _canInteract;
        private bool _activated;
        private InputAction _interactionAction;

        private TextMeshProUGUI _interactText;

        private void Awake()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            
            _animator = GetComponent<Animator>();
            
            _interactionAction = _playerInput.actions.FindAction("Interaction", throwIfNotFound: true);
            
            _canInteract = false;
        }
        
        private void Start()
        {
            var panel = GameObject.Find("InfoPanel");
            _interactText = panel.GetComponentInChildren<TextMeshProUGUI>(true);
            _interactText.gameObject.SetActive(false);
        }

        public void OnInteraction(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            if (!_canInteract) return;
            
            switch (_activated)
            {
                case false:
                    Activate();
                    break;
                case true:
                    SaveGame();
                    break;
            }
        }

        private void OnEnable()
        {
            _canInteract = false;
            
            _activated = GameManager.GameManager.IsCampfireRegistered(campfireSceneName);
            
            if (_activated) _animator.SetBool($"isActivated", true);
            
            _interactionAction.performed += OnInteraction;
            _interactionAction.Enable();
        }

        private void OnDisable()
        {
            _interactionAction.performed -= OnInteraction;
            _interactionAction.Disable();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            _interactText.gameObject.SetActive(true);
            
            _canInteract = true;
            
            _interactText.text = !_activated ? "Press E to activate the campfire" : "Press E to save the game";
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _interactText.text = "";
                _interactText.gameObject.SetActive(false);
                _canInteract = false;
                // TODO: UI jelzés elrejtése
            }
        }
        
        private void Activate()
        {
            _activated = true;
            _animator.SetBool($"isActivated", true);
            
            RefreshAfterActivation();
            
            GameManager.GameManager.RegisterCampfire(campfireSceneName, transform.position);

        }

        private void SaveGame()
        {
            Debug.LogWarning("WHOHOOOOO GAME SAVED");
        }

        private void RefreshAfterActivation()
        {
            if (_canInteract)
            {
                _interactText.text = !_activated ? "Press E to activate the campfire" : "Press E to save the game";
            }
        }
    }
}