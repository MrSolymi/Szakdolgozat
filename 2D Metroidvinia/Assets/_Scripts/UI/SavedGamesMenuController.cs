using System.Collections;
using Solymi._Scripts.GameManager.GameSave;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Solymi._Scripts.UI
{
    public class SavedGamesMenuController : MonoBehaviour
    {
        public Button[] slotButtons;
        public Button[] deleteButtons;
        
        private const string PersistentSceneName = "Persistent";
        private const string QuickPlaySceneName  = "GameStarterScene";
        
        private void OnEnable()
        {
            for (var i = 0; i < slotButtons.Length; i++)
            {
                var slot = i + 1;
                var label = slotButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                
                if (SaveSystem.TryLoad(slot, out var meta))
                    label.text = $"Save {slot} {meta.savedDate}";
                else
                    label.text = "Empty Save";

                slotButtons[i].onClick.RemoveAllListeners();
                slotButtons[i].onClick.AddListener(() => OnSlotClicked(slot));
            }
            
            for (var i = 0; i < deleteButtons.Length; i++)
            {
                var slot = i + 1;
                deleteButtons[i].onClick.RemoveAllListeners();
                deleteButtons[i].onClick.AddListener(() => OnDeleteSlot(slot));
            }
        }
        
        private void OnSlotClicked(int slotIndex)
        {
            StartCoroutine(HandleSlot(slotIndex));
        }
        
        private void OnDeleteSlot(int slotIndex)
        {
            var deleted = SaveSystem.Delete(slotIndex);

            if (deleted && slotIndex <= slotButtons.Length)
            {
                var label = slotButtons[slotIndex - 1].GetComponentInChildren<TextMeshProUGUI>();
                label.text = "Empty Save";
            }
        }
        
        private IEnumerator HandleSlot(int slotIndex)
        {
            var hasSave = SaveSystem.TryLoad(slotIndex, out var meta);

            yield return SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);

            if (hasSave)
            {
                GameManager.GameManager.Instance.LoadGame($"slot{slotIndex}.json");
                
                GameManager.GameManager.Instance.MovePlayerToStartPosition();
                
                GameManager.GameManager.Instance.playingSlot = slotIndex;
                
                yield return SceneManager.LoadSceneAsync(GameManager.GameManager.Instance.currentGameSceneName, LoadSceneMode.Additive);
                
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameManager.GameManager.Instance.currentGameSceneName));
            }
            else
            {
                yield return SceneManager.LoadSceneAsync("GameStarterScene", LoadSceneMode.Additive);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameStarterScene"));
                GameManager.GameManager.Instance.playingSlot = slotIndex;
                //GameManager.GameManager.Instance.currentGameSceneName = "GameStarterScene";
            }
            yield return SceneManager.UnloadSceneAsync("MainMenu");
        }

        public void OnDelete1ButtonClicked() => OnDeleteSlot(1);
        public void OnDelete2ButtonClicked() => OnDeleteSlot(2);
        public void OnDelete3ButtonClicked() => OnDeleteSlot(3);
        public void OnDelete4ButtonClicked() => OnDeleteSlot(4);
    }
}