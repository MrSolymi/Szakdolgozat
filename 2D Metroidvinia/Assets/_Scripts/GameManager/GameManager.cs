using System.Collections.Generic;
using System.IO;
using Solymi._Scripts.GameManager.GameSave;
using Solymi._Scripts.Scene;
using Solymi.Core.CoreComponents;
using Solymi.Enemies.EntityStateMachine;
using Solymi.Enemies.Slime.LittleSlime;
using Solymi.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Solymi._Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public string currentGameSceneName = "";
        public string savedDate = "";

        [Header("Pause Menu")]
        public GameObject pauseMenuUI;
        private bool _isPaused = false;
        private PlayerInput _playerInput;
        
        public GameObject playerDeathUI;

        public int playingSlot;
        
        private static readonly Dictionary<string, Vector2> ActivatedCampfires = new Dictionary<string, Vector2>();
        
        public string savedGameSceneName = "";
        public Vector2 savedGamePosition = Vector2.zero;
        
        private GameObject _playerGameObject;

        public static bool HasActivatedCampfire()
        {
            return ActivatedCampfires.Count > 0;
        }
        
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
            
            _playerGameObject = _playerInput.gameObject;
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
            Time.timeScale = 1f;
            _isPaused = false;
            _playerInput.SwitchCurrentActionMap("Gameplay");

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            EntitySaveTracker.ClearAll();
            ActivatedCampfires.Clear();
            currentGameSceneName = "";
            savedDate = "";
            playingSlot = 0;
            savedGameSceneName = "";
            savedGamePosition = Vector2.zero;
            
            //_playerInput.SwitchCurrentActionMap("Gameplay");
            
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        private void ResetEntities()
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
                entity.ResetAfterSave();
            }
        }

        public void OnRespawnButton()
        {
            ResetEntities();
            
            _playerGameObject.GetComponent<Player.PlayerStateMachine.Player>().Respawn();
        }

        public void MovePlayerToStartPosition()
        {
            var rnd = Mathf.Round(Random.Range(-1.2f, 1.2f) * 100f) / 100f;
            var spawnPos = new Vector2(savedGamePosition.x + rnd, savedGamePosition.y + 0.5f);
            _playerGameObject.transform.position = spawnPos;
        }
        
        public void SaveGame(string filename = "savefile.json")
        {
            var campfireList = new List<CampfireEntry>();
            foreach (var kv in ActivatedCampfires)
            {
                campfireList.Add(new CampfireEntry(kv.Key, kv.Value));
            }

            var data = new SaveData
            {
                activatedCampfires = campfireList,
                savedGameSceneName = savedGameSceneName,
                savedGamePosition = savedGamePosition,
                savedDate = System.DateTime.Now.ToString("yyyy.MM.dd")
            };

            var json = JsonUtility.ToJson(data, prettyPrint: true);

            var path = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllText(path, json);
            Debug.Log($"Game saved to: {path}");
            savedDate = data.savedDate;
            
            ResetEntities();
        }

        public bool LoadGame(string filename = "default.json")
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            if (!File.Exists(path))
            {
                Debug.LogWarning($"Save file not found: {path}");
                return false;
            }

            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Dictionary törlése, majd feltöltése
            ActivatedCampfires.Clear();
            foreach (var entry in data.activatedCampfires)
                ActivatedCampfires[entry.key] = entry.value;

            // Egyéb mezők visszaállítása
            savedGameSceneName = data.savedGameSceneName;
            savedGamePosition = data.savedGamePosition;
            savedDate = data.savedDate;
            
            currentGameSceneName = data.savedGameSceneName;

            Debug.Log($"Game loaded from: {path}");
            return true;
        }
    }
}