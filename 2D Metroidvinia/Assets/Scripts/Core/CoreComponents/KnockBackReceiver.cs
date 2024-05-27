using Solymi.Interfaces;
using UnityEngine;

namespace Solymi.Core.CoreComponents
{
    public class KnockBackReceiver : CoreComponent
    {
        private CoreComponent<Stats> _stats;
        private CoreComponent<Movement> _movement;
        private CoreComponent<CollisionSenses> _collisionSenses;
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        protected override void Awake()
        {
            base.Awake();
            
            _stats = new CoreComponent<Stats>(core);
            _movement = new CoreComponent<Movement>(core);
            _collisionSenses = new CoreComponent<CollisionSenses>(core);
        }
    }
}
