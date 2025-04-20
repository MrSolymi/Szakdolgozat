using System;
using UnityEngine;

namespace Solymi.Core.Stats
{
    [Serializable] public class RespawnPoint
    {
        [field: SerializeField] private Transform defaultRespawnPointTransform;
        [field: SerializeField] private string defaultRespawnPointSceneName;
        public Vector2 DefaultRespawnPoint => defaultRespawnPointTransform.position;
        public string DefaultRespawnPointSceneName => defaultRespawnPointSceneName;
    
        private Vector2 _currentRespawnPoint;
        private string _currentRespawnPointSceneName;
        public Vector2 CurrentRespawnPoint => _currentRespawnPoint;
        public string CurrentRespawnPointSceneName => _currentRespawnPointSceneName;
    
        public void Initialize(GameObject gameObject)
        {
            _currentRespawnPoint = gameObject.transform.position;
        }

        public void Initialize()
        {
            _currentRespawnPoint = defaultRespawnPointTransform.position;
            _currentRespawnPointSceneName = defaultRespawnPointSceneName;
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