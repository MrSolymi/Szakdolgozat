using JetBrains.Annotations;
using Solymi.Core.CoreComponents;
using Solymi.Weapons.Components.Data;
using Solymi.Weapons.Components.Data.AttackData;
using UnityEngine;

namespace Solymi.Weapons.Components
{
    public class WeaponMovement : WeaponComponent<WeaponMovementData, AttackMovement>
    {
        private Movement _coreMovement;
        private Movement CoreMovement => _coreMovement ? _coreMovement : _coreMovement = Core.GetCoreComponent<Movement>();
        
        private CollisionSenses _collisionSenses;
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : _collisionSenses = Core.GetCoreComponent<CollisionSenses>();
        
        //private WeaponMovementData _data;
        // protected override void Awake()
        // {
        //     base.Awake();
        //     
        //     _data = weapon.WeaponData.GetWeaponComponentData<WeaponMovementData>();
        // }

        protected override void Start()
        {
            base.Start();
            
            animationEventHandler.OnStartMovement += HandleStartMovement;
            animationEventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            animationEventHandler.OnStartMovement -= HandleStartMovement;
            animationEventHandler.OnStopMovement -= HandleStopMovement;
        }

        private void HandleStartMovement()
        {
            if (!CollisionSenses.Ground) return; 
            
            //CoreMovement.CanSetVelocity = true;
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }
        private void HandleStopMovement()
        {
            if (!CollisionSenses.Ground) return;
            
            //CoreMovement.CanSetVelocity = false;
            CoreMovement.SetVelocityZero();
        }
    }
}