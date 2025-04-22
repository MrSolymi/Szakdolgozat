using System.IO;
using UnityEngine;

namespace Solymi._Scripts.GameManager.GameSave
{
    public static class SaveSystem
    {
        private const string Prefix = "slot";
        private const string Ext    = ".json";
        
        public static void Save(int slotIndex, SaveData data)
        {
            var json = JsonUtility.ToJson(data, true);
            var filename = Prefix + slotIndex + Ext;
            var path = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllText(path, json);
        }
        
        public static bool TryLoad(int slotIndex, out SaveData data)
        {
            var filename = Prefix + slotIndex + Ext;
            var path = Path.Combine(Application.persistentDataPath, filename);
            if (!File.Exists(path))
            {
                data = null;
                return false;
            }

            var json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            return true;
        }
        
        public static bool Delete(int slotIndex)
        {
            var filename = Prefix + slotIndex + Ext;
            var path = Path.Combine(Application.persistentDataPath, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log($"Deleted save file: {path}");
                return true;
            }
            return false;
        }
    }
}