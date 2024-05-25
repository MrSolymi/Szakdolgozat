using System;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer _baseSpriteRenderer, _weaponSpriteRenderer;
        
        [SerializeField] private WeaponSprites[] _weaponSprites;
        private int _currentIndex;
        
        protected override void Awake()
        {
            base.Awake();
            
            _baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();
            
            
            //TODO: Sometimes this runs before the weapons awake set up the references
            // _baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            // _weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
            weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
            weapon.OnEnter -= HandleEnter;
        }

        private void HandleBaseSpriteChange(SpriteRenderer spriteRenderer)
        {
            if (!isAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }


            if (_currentIndex >= _weaponSprites[weapon.CurrentAttackCounter].Sprites.Length)
            {
                Debug.LogWarning($"Not enough sprites for {weapon.name} attack counter");
                return;
            }
            
            _weaponSpriteRenderer.sprite = _weaponSprites[weapon.CurrentAttackCounter].Sprites[_currentIndex];

            _currentIndex++;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            _currentIndex = 0;
        }
    }

    [Serializable]
    public class WeaponSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}