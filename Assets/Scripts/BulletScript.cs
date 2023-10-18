using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Animator _animator;
    private bool _exploded;
    
    public float speed = 5;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_exploded) return;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy"))
        {
            _animator.SetBool("Explode", true);
            _exploded = true;
            Destroy(gameObject, 0.5f);
        }
    }
}