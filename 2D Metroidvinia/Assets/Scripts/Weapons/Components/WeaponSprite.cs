using System;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
    {
        private SpriteRenderer _baseSpriteRenderer, _weaponSpriteRenderer;
        
        private int _currentIndex;

        // private WeaponSpriteData _data;
        
        protected override void Start()
        {
            base.Start();
            
            _baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
            
            data = weapon.WeaponData.GetWeaponComponentData<WeaponSpriteData>();
            
            _baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        }

        private void HandleBaseSpriteChange(SpriteRenderer spriteRenderer)
        {
            if (!isAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }


            if (_currentIndex >= currentAttackData.Sprites.Length)
            {
                Debug.LogWarning($"Not enough sprites for {weapon.name} attack counter");
                return;
            }
            
            _weaponSpriteRenderer.sprite = currentAttackData.Sprites[_currentIndex];

            _currentIndex++;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            _currentIndex = 0;
        }
    }
}