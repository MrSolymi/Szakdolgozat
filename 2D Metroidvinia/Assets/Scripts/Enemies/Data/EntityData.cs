using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
    public float movementSpeed = 3.0f;
    
    public float minIdleTime = 1.0f;
    public float maxIdleTime = 2.0f;
    
    
}
