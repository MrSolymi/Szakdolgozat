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
            
            //_playerInput.SwitchCurrentActionMap("Gameplay");
            
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}