using System;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    private float _activeTime = 0.1f, _timeActivated, _alpha, _alphaSet = 0.7f, _alphaMultiplier = 0.6f;
    
    private Transform _player;
    
    private SpriteRenderer _spriteRenderer;
    
    private SpriteRenderer _playerSpriteRenderer;
    
    private Color _color;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();
        
        _alpha = _alphaSet;
        _spriteRenderer.sprite = _playerSpriteRenderer.sprite;
        transform.position = _player.position;
        transform.rotation = _player.rotation;
        _timeActivated = Time.time;
    }

    private void Update()
    {
        _alpha *= _alphaMultiplier;
        _color = new Color(1f, 1f, 1f, _alpha);
        _spriteRenderer.color = _color;

        if (Time.time >= (_timeActivated + _activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
