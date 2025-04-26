using System;
using System.Collections;
using System.Globalization;
using Solymi._Scripts.GameManager.GameSave;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solymi._Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel, savedGamesPanel, controlsPanel;
        
        private const int MaxSlots = 4;
        
        public void OnQuickPlay()
        {
            if (TryGetMostRecentSave(out var slotIndex))
            {
                StartCoroutine(LoadSavedGameScenes(slotIndex));
            }
            else
            {
                OnSavedGames();
            }
        }

        public void OnSavedGames()
        {
            menuPanel.SetActive(false);
            savedGamesPanel.SetActive(true);
        }

        public void OnControls()
        {
            menuPanel.SetActive(false);
            controlsPanel.SetActive(true);
        }
        
        public void OnBackToMain()
        {
            savedGamesPanel.SetActive(false);
            controlsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }

        public void OnExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
        
        private bool TryGetMostRecentSave(out int slotIndex)
        {
            slotIndex = -1;
            var bestDate = DateTime.MinValue;

            for (var i = 1; i <= MaxSlots; i++)
            {
                if (SaveSystem.TryLoad(i, out var meta))
                {
                    if (DateTime.TryParseExact(
                            meta.savedDate,
                            "yyyy.MM.dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var date))
                    {
                        if (date > bestDate)
                        {
                            bestDate  = date;
                            slotIndex = i;
                        }
                    }
                }
            }

            return slotIndex != -1;
        }
        
        private IEnumerator LoadSavedGameScenes(int slotIndex)
        {
            yield return SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);
            
            GameManager.GameManager.Instance.LoadGame($"slot{slotIndex}.json");
            GameManager.GameManager.Instance.MovePlayerToStartPosition();
            
            GameManager.GameManager.Instance.playingSlot = slotIndex;
            
            yield return SceneManager.LoadSceneAsync(GameManager.GameManager.Instance.currentGameSceneName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameManager.GameManager.Instance.currentGameSceneName));
            
            yield return SceneManager.UnloadSceneAsync("MainMenu");
        }
    }
}