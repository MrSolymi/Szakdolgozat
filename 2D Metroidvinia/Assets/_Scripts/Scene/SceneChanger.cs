using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solymi._Scripts.Scene
{
    public class SceneChanger : MonoBehaviour
    {
        public string sceneToLoad;
        public string entryPointNameInNewScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FindObjectOfType<FadeController>().FadeToBlackAndLoadScene(sceneToLoad, entryPointNameInNewScene);
            }
        }
    }
}