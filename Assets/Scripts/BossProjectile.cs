using System;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _exploded;
    private PlayerScript _playerScript;
    
    public float speed = 5;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerScript = FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_exploded) return;
        // transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            _animator.SetBool("Explode", true);
            _exploded = true;
            _playerScript.Die();
            Destroy(gameObject, 0.5f);
        }
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            _animator.SetBool("Explode", true);
            _exploded = true;
            _playerScript.GameManager.AddScore(50);
            Destroy(gameObject, 0.2f);
        }
    }
}