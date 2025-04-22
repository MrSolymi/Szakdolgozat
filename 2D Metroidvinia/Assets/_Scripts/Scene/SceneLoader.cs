using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solymi._Scripts.Scene
{
    public class SceneLoader : MonoBehaviour
    {
        private IEnumerator Start()
        {
            const string startScene = "MainMenu";
            yield return SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive);
        }
    }
}