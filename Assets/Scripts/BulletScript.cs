using System;
using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Animator _animator;
    private bool _exploded;

    public float speed = 5;
    public bool left = false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        if (_exploded) return;
        transform.Translate((left ? Vector2.left : Vector2.right) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Decorations") || other.gameObject.CompareTag("player")) return;
        _animator.SetBool("Explode", true);
        _exploded = true;
        // Destroy if collide with anything
        Destroy(gameObject, 0.5f);
    }
    
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Decorations")) return;
    //     if (other.gameObject.CompareTag("player"))
    //     {
    //         // Ignore collision with player
    //         Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
    //         return;
    //     }
    //     _animator.SetBool("Explode", true);
    //     _exploded = true;
    //     // Destroy if collide with anything
    //     Destroy(gameObject, 0.5f);
    // }
}