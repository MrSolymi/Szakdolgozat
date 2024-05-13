using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(_movement, transform.parent.name);
        private set => _movement = value;
    }
    private Movement _movement;

    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(_collisionSenses, transform.parent.name);
        private set => _collisionSenses = value;
    }
    private CollisionSenses _collisionSenses;
    
    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(_combat, transform.parent.name);
        private set => _combat = value;
    }
    private Combat _combat;
    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
    }
    
    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
