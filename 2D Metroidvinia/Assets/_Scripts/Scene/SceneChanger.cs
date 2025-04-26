using System;
using System.Collections;
using Solymi._Scripts.GameManager;
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
                //foreach (var deadEnemy in EntitySaveTracker.GetDeadEnemies())
                //{
                //    Debug.LogError(deadEnemy);
                //}

                FindObjectOfType<FadeController>().FadeToBlackAndLoadScene(sceneToLoad, entryPointNameInNewScene);
            }
        }
    }
}