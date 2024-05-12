using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
    public float movementSpeed = 3.0f;
    
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public LayerMask whatIsGround;
    
    public float minIdleTime = 1.0f;
    public float maxIdleTime = 2.0f;
    
    
}
