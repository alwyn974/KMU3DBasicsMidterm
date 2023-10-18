﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    // -8.279013, -3.40203
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isInvincible;
    private bool _bonusShoot;
    private bool _onSolidSurface;
    private bool _stoppedJumping;
    private bool _dead;
    private GameManager _gameManager;

    public GameObject bulletPrefab;
    public Camera gameCamera;
    public float speed = 7;

    public float jumpForce = 5;
    public float jumpTime = 8;
    private float _jumpTimeCounter;
    private float _shootIntervalCounter;


    public GameManager GameManager => _gameManager;
    public bool IsInvincible => _isInvincible;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _gameManager = GameManager.Instance;
        _gameManager.UpdateText();

        _jumpTimeCounter = jumpTime;
    }

    void Update()
    {
        _shootIntervalCounter -= Time.deltaTime;
        if (_onSolidSurface)
            _jumpTimeCounter = jumpTime;
        _animator.SetBool("BonusShoot", _bonusShoot);

        PlayerJump();
        PlayerShoot();
    }

    void FixedUpdate()
    {
        PlayerFixedWalk();
        CameraFollowPlayer();

        if (_isInvincible)
            _spriteRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 10, 1));
        else
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        
        if (transform.position.y < -10 && !_dead)
        {
            _dead = true;
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Solid"))
        {
            _onSolidSurface = true;
            _animator.SetBool("onSolidSurface", _onSolidSurface);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Solid"))
        {
            _onSolidSurface = false;
            _animator.SetBool("onSolidSurface", _onSolidSurface);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet")) // ignore collision
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
    }

    void PlayerFixedWalk()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis > 0)
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
        else if (horizontalAxis < 0)
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(0, _rb.velocity.y);

        _spriteRenderer.flipX = horizontalAxis < 0;
        _animator.SetInteger("Speed", Mathf.Abs((int)_rb.velocity.x));
    }

    void PlayerJump()
    {
        if (!_onSolidSurface) return;
        var isJumping = Input.GetButton("Jump");
        if (isJumping)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _stoppedJumping = false;

            if (!_stoppedJumping && _jumpTimeCounter > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
                _jumpTimeCounter -= Time.deltaTime;
            }
        }
        else
        {
            _jumpTimeCounter = 0;
            _stoppedJumping = true;
        }

        _animator.SetBool("Jump", isJumping);
    }

    void PlayerShoot()
    {
        if (!_bonusShoot) return;
        // if shift or ctrl is pressed ?
        if (!Input.GetButtonDown("Fire1")) return;
        // cooldown
        if (_shootIntervalCounter > 0) return;
        _shootIntervalCounter = 1f;
        
        Instantiate(bulletPrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
    }

    public void Die()
    {
        _gameManager.AddLife(-1);
        if (_gameManager.Life <= 0)
        {
            Debug.Log("Game Over");
            // TODO: Game Over
        }
        else
        {
            Debug.Log("Respawn");
            StartCoroutine(ResetScene());
        }
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPlayerGetBonus(BonusType type)
    {
        switch (type)
        {
            case BonusType.Shield:
                _gameManager.AddScore(1000);
                StartCoroutine(Invincible());
                break;
            case BonusType.Flower:
                _gameManager.AddScore(200);
                // StartCoroutine(BonusShoot());
                _bonusShoot = true;
                _animator.SetBool("BonusShoot", _bonusShoot);
                break;
            case BonusType.Coin:
                _gameManager.AddCoin();
                break;
        }
    }

    private IEnumerator Invincible()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(7);
        _isInvincible = false;
    }

    private IEnumerator BonusShoot()
    {
        _bonusShoot = true;
        _animator.SetBool("BonusShoot", _bonusShoot);
        yield return new WaitForSeconds(10);
        _bonusShoot = false;
        _animator.SetBool("BonusShoot", _bonusShoot);
    }

    void CameraFollowPlayer()
    {
        var cameraPosition = gameCamera.transform.position;
        cameraPosition = Vector3.Lerp(cameraPosition,
            new Vector3(transform.position.x, cameraPosition.y, cameraPosition.z), 0.05f);
        if (cameraPosition.x < 0)
            cameraPosition = new Vector3(0, cameraPosition.y, cameraPosition.z);
        gameCamera.transform.position = cameraPosition;
    }

    public void NextLevel()
    {
        if (_gameManager.Level == Level.Three)
        {
            _gameManager.AddScore((int) _gameManager.Level * 2000);
            Debug.Log("You win");
            // TODO: Win
            return;
        }

        _gameManager.BossKilled[_gameManager.Level] = true;
        _gameManager.Level++;
        _gameManager.AddScore((int) _gameManager.Level * 2000);
        SceneManager.LoadScene($"Level0{(int)_gameManager.Level + 1}Scene");
    }
}