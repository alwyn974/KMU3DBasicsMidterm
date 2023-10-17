using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SnailScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _stunned;
    public PlayerScript playerScript;
    public float speed = -0.25f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (_stunned) return;
        _rb.velocity = new Vector2(speed, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_stunned) return;
        
        if (other.gameObject.CompareTag("ColliderBack"))
        {
            var velocity = _rb.velocity;
            speed = -speed;
            _rb.velocity = new Vector2(speed, velocity.y);
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        if (other.gameObject.CompareTag("player"))
        {
            // check if player has collided from top
            if (other.gameObject.transform.position.y > transform.position.y + 0.5f)
            {
                playerScript.InventoryManager.AddScore(200);
                _stunned = true;
                _animator.SetBool("Stunned", true);
                _collider2D.enabled = false;
                _rb.bodyType = RigidbodyType2D.Static;
                
                Destroy(gameObject, 2.5f);
                return;
            }
            if (playerScript.IsInvincible) return;
            
            playerScript.Die();
        }
    }
}
