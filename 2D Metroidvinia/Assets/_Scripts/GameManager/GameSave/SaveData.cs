using System.Collections.Generic;
using UnityEngine;

namespace Solymi._Scripts.GameManager.GameSave
{
    [System.Serializable]
    public class SaveData
    {
        public List<CampfireEntry> activatedCampfires;
        public string savedGameSceneName;
        public Vector2 savedGamePosition;
        public string savedDate; //yyyy.MM.dd format
    }
}