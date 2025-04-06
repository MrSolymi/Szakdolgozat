using System;
using UnityEngine;
using UnityEngine.UI;

namespace Solymi.Core.CoreComponents
{
    public class HealthBar : CoreComponent
    {
        private Slider _healthSlider;
        private Slider _lerpHealthSlider;
        private Stats _stats;
        [SerializeField] private float lerpSpeed = 5f;
        protected override void Awake()
        {
            base.Awake();
            
            var sliders = GetComponentsInChildren<Slider>();
            
            _stats = core.GetCoreComponent<Stats>();

            foreach (var slider in sliders)
            {
                switch (slider.transform.name)
                {
                    case "Health bar":
                        _healthSlider = slider;
                        break;
                    case "Lerp health bar":
                        _lerpHealthSlider = slider;
                        break;
                }
            }
            
            _healthSlider.value = 100;
            _lerpHealthSlider.value = 100;
            
            gameObject.SetActive(false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_stats.Health.CurrentValue != _stats.Health.MaxValue)
            {
                gameObject.SetActive(true);
            }
            
            // Debug.LogWarning(_stats.Health.MaxValue + " " + _stats.Health.CurrentValue);
            
            _healthSlider.value = (_stats.Health.CurrentValue / _stats.Health.MaxValue) * 100;
            _lerpHealthSlider.value = Mathf.Lerp(_lerpHealthSlider.value, _healthSlider.value, Time.deltaTime * lerpSpeed);
        }

        public void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}