using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solymi._Scripts.GameManager
{
    [ExecuteAlways]
    public class UniqueId : MonoBehaviour
    {
        [SerializeField] private string id;

        public string Id => id;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                if (string.IsNullOrEmpty(id) || IdExistsInScene(id))
                {
                    id = GenerateUniqueId();
                    UnityEditor.EditorUtility.SetDirty(this);
                }
            }
        }

        private bool IdExistsInScene(string checkId)
        {
            UniqueId[] all = FindObjectsOfType<UniqueId>(true);
            foreach (var other in all)
            {
                if (other == this) continue;
                if (other.Id == checkId) return true;
            }
            return false;
        }

        private string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }
#endif
    }
}