using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private CircleCollider2D _groundCollider;
    private InventoryManager _inventoryManager;
    private bool _isInvincible = false;
    private bool _bonusShoot = false;
    private bool _onSolidSurface = false;
    private float _jumpTime;
    private bool _jumping;
    private bool _jumpCancelled;

    public Camera gameCamera;
    public float speed = 5;
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    public float cancelRate = 100;


    public InventoryManager InventoryManager => _inventoryManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _groundCollider = GetComponent<CircleCollider2D>();
        _inventoryManager = GetComponent<InventoryManager>();
    }

    void Update()
    {
        PlayerUpdateJump();
    }

    void FixedUpdate()
    {
        PlayerFixedWalk();
        PlayerFixedJump();
        CameraFollowPlayer();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Solid"))
        {
            _onSolidSurface = true;
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

    void PlayerFixedJump()
    {
        if (_jumpCancelled && _jumping && _rb.velocity.y > 0)
            _rb.AddForce(Vector2.down * cancelRate);
    }

    void PlayerUpdateJump()
    {
        if (Input.GetButton("Jump"))
        {
            var jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rb.gravityScale));
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            _animator.SetBool("Jump", true);
            _jumping = true;
            _jumpCancelled = false;
            _jumpTime = 0;
            _jumpTime += Time.deltaTime;
        }

        if (_jumping)
        {
            _jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _jumpCancelled = true;
            }
            if (_jumpTime > buttonTime)
            {
                _jumping = false;
                _animator.SetBool("Jump", false);
            }
        }
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