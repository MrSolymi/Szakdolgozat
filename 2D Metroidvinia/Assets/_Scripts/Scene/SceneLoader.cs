using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solymi._Scripts.Scene
{
    public class SceneLoader : MonoBehaviour
    {
        private IEnumerator Start()
        {
            //////////SceneManager.LoadScene($"Persistent", LoadSceneMode.Additive);
//////////
            //////////SceneManager.LoadScene($"GameStarterScene", LoadSceneMode.Additive);
            
            // Persistent scene betöltése először
            //yield return SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);

            //// Első game scene neve
            //const string startScene = "GameStarterScene";
//
            //// Első game scene betöltése
            //yield return SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive);
//
            //// Aktív scene beállítása
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName(startScene));

            // GameManager-be beállítás
            //if (GameManager.GameManager.Instance != null)
            //{
            //    GameManager.GameManager.Instance.currentGameSceneName = startScene;
            //}
            
            const string startScene = "MainMenu";
            yield return SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive);
        }
    }
}