using System;
using Solymi.Core.CoreComponents;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Solymi._Scripts.GameManager;

namespace Solymi._Scripts.Scene
{
    public class Campfire : MonoBehaviour
    {
        private string _campfireSceneName; 
        
        private Animator _animator;
        private PlayerInput _playerInput;
        private bool _canInteract;
        private bool _activated;
        private InputAction _interactionAction;

        private TextMeshProUGUI _interactText;
        
        private Stats _playerStats;

        private void Awake()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            
            _animator = GetComponent<Animator>();
            
            _interactionAction = _playerInput.actions.FindAction("Interaction", throwIfNotFound: true);
            
            _canInteract = false;
        }

        private void Update()
        {
            if (_canInteract && _activated && _playerStats)
            {
                _playerStats.Health.Increase(0.1f);
            }
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
            
            //Debug.LogError("Interaction called");
            
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
            _campfireSceneName = gameObject.scene.name;
            
            _canInteract = false;
            
            _activated = GameManager.GameManager.IsCampfireRegistered(_campfireSceneName);
            
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
            
            _playerStats = other.gameObject.GetComponent<Player.PlayerStateMachine.Player>().Core.GetCoreComponent<Stats>();
            
            //Debug.Log(_playerStats.Health.CurrentValue);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _interactText.text = "";
                _interactText.gameObject.SetActive(false);
                _canInteract = false;
            }
        }
        
        private void Activate()
        {
            _activated = true;
            _animator.SetBool($"isActivated", true);
            
            RefreshAfterActivation();
            
            GameManager.GameManager.RegisterCampfire(_campfireSceneName, transform.position);

        }

        private void SaveGame()
        {
            _playerStats.RespawnPoint.SetRespawnPoint(GameManager.GameManager.GetCampfirePosition(_campfireSceneName));
            _playerStats.RespawnPoint.SetRespawnPointSceneName(_campfireSceneName);
            
            GameManager.GameManager.Instance.savedGameSceneName = _playerStats.RespawnPoint.CurrentRespawnPointSceneName;
            GameManager.GameManager.Instance.savedGamePosition.Set(_playerStats.RespawnPoint.CurrentRespawnPoint.x, _playerStats.RespawnPoint.CurrentRespawnPoint.y);
            
            GameManager.GameManager.Instance.SaveGame($"slot{GameManager.GameManager.Instance.playingSlot}.json");
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