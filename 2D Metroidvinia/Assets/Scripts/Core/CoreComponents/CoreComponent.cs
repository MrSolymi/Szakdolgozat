using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core core;

    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();

        if (!core)
        {
            Debug.LogError("There is no Core on the parent object of " + this + " component.");
        }
        core.AddComponent(this);
    }

    public virtual void LogicUpdate()
    {
        
    }
}
