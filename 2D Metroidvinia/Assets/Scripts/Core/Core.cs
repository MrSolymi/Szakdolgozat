using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private List<ILogicUpdate> _components = new List<ILogicUpdate>();
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
    
    public Stats Stats
    {
        get => GenericNotImplementedError<Stats>.TryGet(_stats, transform.parent.name);
        private set => _stats = value;
    }
    private Stats _stats;
    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
        Stats = GetComponentInChildren<Stats>();
    }
    
    public void LogicUpdate()
    {
        foreach (var component in _components)
        {
            component.LogicUpdate();
        }
    }
    
    public void AddComponent(ILogicUpdate component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
        }
    }
}
