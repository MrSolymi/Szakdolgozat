using System;
using UnityEngine;

public class CombatDummy : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
