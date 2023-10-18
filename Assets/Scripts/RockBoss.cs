using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RockBoss : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private CapsuleCollider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerDetect _playerDetect;
    private float _shootIntervalCounter;
    private bool _shooting;
    private bool _dead;

    public int life = 50;
    public GameObject rockPrefab;
    // public float speed = 0.5f;
    public float shootInterval = 5f;
    public PlayerScript playerScript;
    public Level bossLevel;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerDetect = GetComponentInChildren<PlayerDetect>();
    }

    private void Update()
    {
        // detect player entering boss area
        if (_playerDetect.playerDetected)
        {
            if (_shooting) return;
            _shooting = true;
            StartCoroutine(Shoot());
        }
        else
            _shooting = false;

        if (_dead)
        {
            _animator.SetTrigger("Death");
        }
    }

    private IEnumerator Shoot()
    {
        while (!_dead)
        {
            if (_shooting)
            {
                _animator.SetTrigger("Shooting");
                // wait mid animation
                yield return new WaitForSeconds(0.5f);
                // shoot rock projectile from top of boss
                var rock = Instantiate(rockPrefab, transform.position + new Vector3(-1, 0.5f, 0), Quaternion.identity);
                // add velocity to have a parabolic trajectory
                rock.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * 5;
                // rock.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * 5;
                yield return new WaitForSeconds(shootInterval);
                _animator.Play("RockBossIdle");
            }
        }
    }

    private IEnumerator SpriteBlink()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_dead) return;
        // instant kill player
        if (other.gameObject.CompareTag("player"))
        {
            playerScript.Die();
        }
        
        if (other.gameObject.CompareTag("Bullet") && _playerDetect.playerDetected)
        {
            // remove 10 life
            life -= 10;
            // blink sprite in red
            StartCoroutine(SpriteBlink());

            if (life < 0)
            {
                _dead = true;
                _animator.SetTrigger("Death");
                _collider2D.enabled = false;
                _rb.bodyType = RigidbodyType2D.Static;
                playerScript.GameManager.BossKilled[bossLevel] = true;
            }
            Destroy(other.gameObject, 0.5f); // bullet
        }
    }
}