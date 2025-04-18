using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solymi._Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel, savedGamesPanel, controlsPanel;
        
        public void OnQuickPlay()
        {
            StartCoroutine(LoadQuickPlayScenes());
        }

        public void OnSavedGames()
        {
            //Debug.Log("Saved Games menü megnyitása...");
            menuPanel.SetActive(false);
            savedGamesPanel.SetActive(true);
        }

        public void OnControls()
        {
            //Debug.Log("Controls menü megnyitása...");
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
            // Ha az Editorban teszteled, ezt is hívd meg, hogy megálljon a Play módban:
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
            // Ha a játék buildben fut, kilép:
            Application.Quit();
        }

        private IEnumerator LoadQuickPlayScenes()
        {
            yield return SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);
            const string startScene = "GameStarterScene";
            yield return SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(startScene));
            if (GameManager.GameManager.Instance != null)
            {
                GameManager.GameManager.Instance.currentGameSceneName = startScene;
            }
            yield return SceneManager.UnloadSceneAsync("MainMenu");
        }
    }
}