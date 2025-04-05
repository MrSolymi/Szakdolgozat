using UnityEngine;

namespace Solymi.Interfaces
{
    public interface IKnockBackable
    {
        void KnockBack(Vector2 knockBackAngle, float knockBackStrength, int direction);
    }
}