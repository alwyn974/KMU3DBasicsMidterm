using System;
using UnityEngine;

public class GoombaScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _stunned;
    public PlayerScript playerScript;
    public float speed = -0.25f;
    public GoombaType type;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ColliderBack"))
        {
            var velocity = _rb.velocity;
            speed = -speed;
            _rb.velocity = new Vector2(speed, velocity.y);
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Die();
            Destroy(other.gameObject, 0.5f); // bullet
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_stunned) return;
        // ignore collisions with other enemies
        if (other.gameObject.CompareTag("Enemy"))
            Physics2D.IgnoreCollision(_collider2D, other.collider);

        if (other.gameObject.CompareTag("player"))
        {
            // check if player has collided from top
            if (other.gameObject.transform.position.y > transform.position.y + 0.5f || playerScript.IsInvincible)
            {
                Die();
                return;
            }
            // if (playerScript.IsInvincible) return;

            playerScript.Die();
        }
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            Die();
            Destroy(other.gameObject, 0.5f); // bullet
        }
    }

    private void Die(bool bullet = false)
    {
        playerScript.GameManager.AddScore(((int)type) + (bullet ? -10 : 0));
        playerScript.PlaySound(GameManager.Instance.StompSound);
        _stunned = true;
        _animator.SetBool("Stunned", true);
        _collider2D.enabled = false;
        _rb.bodyType = RigidbodyType2D.Static;

        Destroy(gameObject, 1f);
    }
}

public enum GoombaType
{
    Snail = 100,
    Beetle = 200,
    Turtle = 300,
    Ghost = 400
}