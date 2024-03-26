using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        if (!Movement)
        {
            Debug.LogError("There is no Movement component on the parent object of " + this + " component.");
        }
        
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        if (!CollisionSenses)
        {
            Debug.LogError("There is no CollisionSenses component on the parent object of " + this + " component.");
        }
    }
    
    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
