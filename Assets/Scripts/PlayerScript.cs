using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private Animator _animator;
    private bool _isInvincible;
    private bool _bonusShoot;
    private bool _onSolidSurface;
    private bool _stoppedJumping;
    private InventoryManager _inventoryManager;

    public Camera gameCamera;
    public float speed = 7;

    public float jumpForce = 5;
    public float jumpTime = 8;
    private float _jumpTimeCounter;


    public InventoryManager InventoryManager => _inventoryManager;
    public bool IsInvincible => _isInvincible;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _inventoryManager = InventoryManager.Instance;
        _inventoryManager.UpdateText();

        _jumpTimeCounter = jumpTime;
    }

    void Update()
    {
        if (_onSolidSurface)
            _jumpTimeCounter = jumpTime;

        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerFixedWalk();
        CameraFollowPlayer();
        
        if (_isInvincible)
            _spriteRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 10, 1));
        
        if (transform.position.y < -10)
            Die();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ColliderBack"))
        {
            Physics2D.IgnoreCollision(other.collider, _collider2D);
            return;
        }
        
        if (other.gameObject.CompareTag("Solid") && other.contacts[0].normal.y > 0.5f)
        {
            _onSolidSurface = true;
            _animator.SetBool("onSolidSurface", _onSolidSurface);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Solid"))
        {
            _onSolidSurface = false;
            _animator.SetBool("onSolidSurface", _onSolidSurface);
        }
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

    public void Die()
    {
        _inventoryManager.AddLife(-1);
        if (_inventoryManager.Life <= 0)
        {
            Debug.Log("Game Over");
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
        if (type == BonusType.Flower)
        {
            Debug.Log("shoot bonus");
        }
        else if (type == BonusType.Shield)
        {
            Debug.Log("Invincible bonus");
        }
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
}