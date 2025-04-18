using System;
using System.Collections;
using Solymi.Core.CoreComponents;
using Solymi.Player.Input;
using Solymi.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Solymi._Scripts.Scene
{
    public class FadeController : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private Image fadeImage;
        [SerializeField] private float waitBeforeFadeOut = 1f;
        
        private Timer _timer;
        private PlayerInput _playerInput;

        public void FadeToBlackAndLoadScene(string sceneName, string entryPointName)
        {
            StartCoroutine(FadeAndSwitch(sceneName, entryPointName));
        }

        private void Awake()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }

        private IEnumerator FadeAndSwitch(string sceneName, string entryPointName)
        {
            _playerInput.SwitchCurrentActionMap("EmptyActionMap");
            yield return StartCoroutine(Fade(1)); // Fade to black

            // unload korábbi scene, ha nem "Persistent"
            string prev = GameManager.GameManager.Instance.currentGameSceneName;
            if (!string.IsNullOrEmpty(prev) && prev != "Persistent" && SceneManager.GetSceneByName(prev).isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(prev);
            }

            // új scene betöltése ADDITIVELY
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!loadOp.isDone)
                yield return null;

            // új scene legyen az aktív
            UnityEngine.SceneManagement.Scene newScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(newScene);

            // frissítjük a GameManager-ben az aktív scene nevet
            GameManager.GameManager.Instance.currentGameSceneName = sceneName;

            // entryPoint regisztráció lefutásához várunk egy frame-et
            yield return null;

            // játékos mozgatás
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null)
            {
                Vector2 pos = EntryPointDatabase.GetEntryPointPosition(entryPointName);
                player.transform.position = pos;
            }
            
            bool finished = false;
            _timer = new Timer(waitBeforeFadeOut);
            _timer.OnTimerEnd += () => finished = true;
            _timer.StartTimer();

            while (!finished)
            {
                _timer.Tick();
                yield return null;
            }

            yield return StartCoroutine(Fade(0)); // Fade in
            _playerInput.SwitchCurrentActionMap("Gameplay");
        }
        
        private IEnumerator Fade(float targetAlpha)
        {
            var color = fadeImage.color;
            var startAlpha = color.a;
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime / fadeDuration;
                color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
                fadeImage.color = color;
                yield return null;
            }
        }
    }
}