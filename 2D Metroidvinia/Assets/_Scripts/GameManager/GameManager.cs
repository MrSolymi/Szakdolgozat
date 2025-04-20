using System.Collections.Generic;
using Solymi._Scripts.Scene;
using Solymi.Core.CoreComponents;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.Slime.LittleSlime;
using Solymi.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Solymi._Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public string currentGameSceneName = "";

        [Header("Pause Menu")]
        public GameObject pauseMenuUI;
        private bool _isPaused = false;
        private PlayerInput _playerInput;
        
        public GameObject playerDeathUI;
        
        private static readonly Dictionary<string, Vector2> ActivatedCampfires = new Dictionary<string, Vector2>();
        
        public static void RegisterCampfire(string name, Vector2 position)
        {
            ActivatedCampfires[name] = position;
        }
        
        public static Vector2 GetCampfirePosition(string name)
        {
            if (ActivatedCampfires.TryGetValue(name, out var pos))
            {
                return pos;
            }

            Debug.LogWarning("Entry point not found: " + name);
            return Vector2.zero;
        }

        public static bool IsCampfireRegistered(string name)
        {
            return ActivatedCampfires.ContainsKey(name);
        }
        
        public static void ClearCampfires() => ActivatedCampfires.Clear();
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
            
            _playerInput = FindObjectOfType<PlayerInput>();
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(false);
            if (playerDeathUI != null)
                playerDeathUI.SetActive(false);
        }
        
        // Ez kell az Input System Pause action-jához
        public void OnPause(InputAction.CallbackContext context)
        {
            if (!context.started || pauseMenuUI == null) 
                return;

            if (_isPaused) 
                Resume();
            else 
                Pause();
        }
        
        private void Pause()
        {
            // 1) UI panel mutatása
            pauseMenuUI.SetActive(true);
            // 2) Váltunk a UI Action Map‑re
            _playerInput.SwitchCurrentActionMap("UI");
            // 3) Játék megállítása
            Time.timeScale = 0f;
            _isPaused = true;

            // 4) (Fontos!) Mutasd a kurzort, és oldd fel a lock‑ot
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Resume()
        {
            // 1) UI panel elrejtése
            pauseMenuUI.SetActive(false);
            // 2) Vissza a játékmenethez Action Map‑ben
            _playerInput.SwitchCurrentActionMap("Gameplay");
            // 3) Játékidő folytatása
            Time.timeScale = 1f;
            _isPaused = false;

            // 4) (Opcionális) Elrejtheted a kurzort, és lockolhatod újra
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        // Ez a Resume gomb OnClick eseményéhez
        public void OnResumeButton()
        {
            Resume();
        }
        
        public void OnBackToMainMenu()
        {
            Resume();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            EntitySaveTracker.ClearAll();
            ActivatedCampfires.Clear();
            
            //_playerInput.SwitchCurrentActionMap("Gameplay");
            
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void OnSaveGameButton()
        {
            EntitySaveTracker.ClearAll();
            
            var littleSlimes = GameObject.FindObjectsOfType<LittleSlime>();
            foreach (var littleSlime in littleSlimes)
            {
                GameObject.Destroy(littleSlime.gameObject);
            }
            
            var entities = GameObject.FindObjectsOfType<Entity>(true);

            foreach (var entity in entities)
            {
                entity.gameObject.SetActive(true);
                entity.ResetAfterSave();
            }
        }

        public void OnRespawnButton()
        {
            EntitySaveTracker.ClearAll();
            
            var littleSlimes = GameObject.FindObjectsOfType<LittleSlime>();
            foreach (var littleSlime in littleSlimes)
            {
                GameObject.Destroy(littleSlime.gameObject);
            }
            
            var entities = GameObject.FindObjectsOfType<Entity>(true);
            
            foreach (var entity in entities)
            {
                //entity.gameObject.SetActive(true);
                entity.ResetAfterSave();
            }
            
            var player = GameObject.FindObjectOfType<Player.PlayerStateMachine.Player>(true);
            
            player.Respawn();
        }
    }
}