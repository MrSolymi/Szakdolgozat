using System.Collections.Generic;
using UnityEngine;

namespace Solymi._Scripts.Scene
{
    public class EntryPointDatabase
    {
        private static readonly Dictionary<string, Vector2> EntryPoints = new Dictionary<string, Vector2>();

        public static void RegisterEntryPoint(string name, Vector2 position)
        {
            EntryPoints[name] = position;
        }

        public static Vector2 GetEntryPointPosition(string name)
        {
            if (EntryPoints.TryGetValue(name, out var pos))
            {
                return pos;
            }

            Debug.LogWarning("Entry point not found: " + name);
            return Vector2.zero;
        }

        public static void Clear() => EntryPoints.Clear();
    }
}