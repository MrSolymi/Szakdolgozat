using UnityEngine;

namespace Solymi._Scripts.GameManager.GameSave
{
    [System.Serializable]
    public class CampfireEntry
    {
        public string key;
        public Vector2 value;

        public CampfireEntry(string k, Vector2 v)
        {
            key = k;
            value = v;
        }
    }
}