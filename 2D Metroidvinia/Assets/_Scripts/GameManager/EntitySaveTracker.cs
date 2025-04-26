using System.Collections.Generic;

namespace Solymi._Scripts.GameManager
{
    public static class EntitySaveTracker
    {
        private static HashSet<string> _deadEnemies = new HashSet<string>();

        public static void RegisterDeath(string id)
        {
            if (!_deadEnemies.Contains(id))
                _deadEnemies.Add(id);
        }

        public static bool IsDead(string id) => _deadEnemies.Contains(id);

        public static void ClearAll()
        {
            _deadEnemies.Clear();
        }
        
        public static HashSet<string> GetDeadEnemies() => _deadEnemies;
    }
}