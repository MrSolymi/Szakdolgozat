using UnityEngine;

namespace Solymi._Scripts.Scene
{
    public class EntryPoint : MonoBehaviour
    {
        public string entryPointName;

        private void Awake()
        {
            EntryPointDatabase.RegisterEntryPoint(entryPointName, transform.position);
        }
    }
}