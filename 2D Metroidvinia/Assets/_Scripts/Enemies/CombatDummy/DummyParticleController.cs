using UnityEngine;

namespace Solymi.Enemies.CombatDummy
{
    public class DummyParticleController : MonoBehaviour
    {
        private void FinishAnimation()
        {
            Destroy(gameObject);
        }
    }
}
