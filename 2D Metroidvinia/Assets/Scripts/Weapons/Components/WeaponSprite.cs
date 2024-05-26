using System;
using Solymi.Weapons.Components.Data;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer _baseSpriteRenderer, _weaponSpriteRenderer;
        
        private int _currentIndex;

        private WeaponSpriteData _data;
        
        protected override void Awake()
        {
            base.Awake();
            
            _data = weapon.WeaponData.GetWeaponComponentData<WeaponSpriteData>();
            
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


            if (_currentIndex >= _data.AttackData[weapon.CurrentAttackCounter].Sprites.Length)
            {
                Debug.LogWarning($"Not enough sprites for {weapon.name} attack counter");
                return;
            }
            
            _weaponSpriteRenderer.sprite = _data.AttackData[weapon.CurrentAttackCounter].Sprites[_currentIndex];

            _currentIndex++;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            _currentIndex = 0;
        }
    }
}