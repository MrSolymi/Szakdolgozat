using System;
using UnityEngine;

namespace Solymi.Core.Stats
{
    [Serializable] public class RespawnPoint
    {
        [field: SerializeField] private Transform defaultRespawnPointTransform;
        public Vector2 DefaultRespawnPoint => defaultRespawnPointTransform.position;
    
        private Vector2 _currentRespawnPoint;
        public Vector2 CurrentRespawnPoint => _currentRespawnPoint;
    
        public void Initialize(GameObject gameObject)
        {
            _currentRespawnPoint = gameObject.transform.position;
        }
    
        public void Initialize(Vector2 respawnPoint)
        {
            _currentRespawnPoint = respawnPoint;
        }
    
        public void SetRespawnPoint(Vector2 respawnPoint)
        {
            _currentRespawnPoint = respawnPoint;
        }
    
        public void Reset()
        {
            _currentRespawnPoint = defaultRespawnPointTransform.position;
        }

        public void Respawn(Transform transform)
        {
            transform.position = _currentRespawnPoint;
        }
    }
}